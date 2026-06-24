package com.example.finalproject.viewmodel

import androidx.lifecycle.*
import com.example.finalproject.Apartament
import com.example.finalproject.data.ApartmentDao
import kotlinx.coroutines.launch


class DataBaseViewModelFactory(private val dao: ApartmentDao) : ViewModelProvider.Factory {
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        if (modelClass.isAssignableFrom(DataBaseViewModel::class.java)) {
            @Suppress("UNCHECKED_CAST")
            return DataBaseViewModel(dao) as T
        }
        throw IllegalArgumentException("Unknown ViewModel class")
    }
}

class DataBaseViewModel(private val dao: ApartmentDao) : ViewModel() {

    val apartments: LiveData<List<Apartament>> = dao.getAll().asLiveData()

    fun addApartment(apartment: Apartament) {
        viewModelScope.launch {
            dao.insert(apartment)
        }
    }

    suspend fun DeleteApp(number: Int) {
        dao.deleteByNumber(number)
    }

    suspend fun findApp(number: Int): Boolean {
        return dao.findByNumber(number) != null
    }

    suspend fun UpdateApp(apartment: Apartament): Boolean {
        return try {
            dao.update(apartment)
            true
        } catch (e: Exception) {
            false
        }
    }

    suspend fun getCount(number: Int): Int {
        return dao.findCountByNumber(number)
    }
}