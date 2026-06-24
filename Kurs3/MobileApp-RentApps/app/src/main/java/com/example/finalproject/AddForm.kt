package com.example.finalproject

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.example.finalproject.viewmodel.ApartmentViewModel
import com.example.finalproject.viewmodel.DataBaseViewModel
import com.google.android.material.switchmaterial.SwitchMaterial
import com.google.android.material.textfield.TextInputEditText
import kotlinx.coroutines.launch

class AddForm : Fragment() {

    // Используем общую ViewModel из MainActivity
    private val viewModel: DataBaseViewModel by activityViewModels {
        (activity as MainActivity).viewModelFactory
    }
    // Объявляем View как переменные уровня класса для удобного доступа
    private lateinit var numberEditText: TextInputEditText
    private lateinit var areaEditText: TextInputEditText
    private lateinit var roomsEditText: TextInputEditText
    private lateinit var rentEditText: TextInputEditText
    private lateinit var rentedSwitch: SwitchMaterial

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return inflater.inflate(R.layout.fragment_add_form, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        // Инициализация View
        numberEditText = view.findViewById(R.id.number_edit_text)
        areaEditText = view.findViewById(R.id.area_edit_text)
        roomsEditText = view.findViewById(R.id.rooms_edit_text)
        rentEditText = view.findViewById(R.id.rent_edit_text)
        rentedSwitch = view.findViewById(R.id.rented_switch)

        val addButton = view.findViewById<Button>(R.id.addBut)
        val delButton = view.findViewById<Button>(R.id.delBut)
        val changeButton = view.findViewById<Button>(R.id.ChangeBut)
        val findButton = view.findViewById<Button>(R.id.FindBut)

        // КНОПКА ДОБАВИТЬ
        addButton.setOnClickListener {
            val apartment = gatherInput()
            if (apartment != null) {
                lifecycleScope.launch {
                    if (!viewModel.findApp(apartment.ApartamentNumb)) {
                        viewModel.addApartment(apartment)
                        showToast("Квартира №${apartment.ApartamentNumb} добавлена")
                        findNavController().navigateUp()
                    } else {
                        showToast("Ошибка: Квартира №${apartment.ApartamentNumb} уже существует")
                    }
                }
            }
        }

        // КНОПКА УДАЛИТЬ
        delButton.setOnClickListener {
            val numberStr = numberEditText.text.toString()
            if (numberStr.isEmpty()) {
                showToast("Введите номер квартиры для удаления")
                return@setOnClickListener
            }

            val num = numberStr.toIntOrNull() ?: return@setOnClickListener
            lifecycleScope.launch {
                if (viewModel.findApp(num)) {
                    viewModel.DeleteApp(num)
                    showToast("Квартира №$num удалена")
                    findNavController().navigateUp()
                } else {
                    showToast("Квартира не найдена")
                }
            }
        }

        // КНОПКА ИЗМЕНИТЬ
        changeButton.setOnClickListener {
            val apartment = gatherInput()
            if (apartment != null) {
                lifecycleScope.launch {
                    if (viewModel.findApp(apartment.ApartamentNumb)) {
                        viewModel.UpdateApp(apartment)
                        showToast("Данные обновлены")
                        findNavController().navigateUp()
                    } else {
                        showToast("Квартира не найдена для обновления")
                    }
                }
            }
        }

        // КНОПКА НАЙТИ
        findButton.setOnClickListener {
            val numberStr = numberEditText.text.toString()
            if (numberStr.isEmpty()) {
                showToast("Введите номер для поиска")
                return@setOnClickListener
            }

            val num = numberStr.toIntOrNull() ?: return@setOnClickListener
            lifecycleScope.launch {
                if (viewModel.findApp(num)) {
                    showToast("Квартира №$num найдена в базе")
                } else {
                    showToast("Квартира не найдена")
                }
            }
        }
    }


    private fun gatherInput(): Apartament? {
        val nStr = numberEditText.text.toString()
        val aStr = areaEditText.text.toString()
        val rStr = roomsEditText.text.toString()
        val rentStr = rentEditText.text.toString()

        if (nStr.isEmpty() || aStr.isEmpty() || rStr.isEmpty() || rentStr.isEmpty()) {
            showToast("Заполните все поля!")
            return null
        }

        // Используем toIntOrNull, чтобы приложение не падало при вводе текста в числовое поле
        val num = nStr.toIntOrNull()
        val area = aStr.toDoubleOrNull()
        val rooms = rStr.toIntOrNull()
        val rent = rentStr.toIntOrNull()

        if (num == null || area == null || rooms == null || rent == null) {
            showToast("Ошибка ввода: проверьте числовые поля")
            return null
        }

        return Apartament(
            ApartamentNumb = num,
            Area = area,
            Rent = rent,
            CntRooms = rooms,
            IsRented = rentedSwitch.isChecked
        )
    }

    private fun showToast(message: String) {
        Toast.makeText(requireContext(), message, Toast.LENGTH_SHORT).show()
    }
}