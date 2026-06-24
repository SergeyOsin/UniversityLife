// ApartmentViewModel.kt
package com.example.finalproject.viewmodel

import androidx.lifecycle.*
import com.example.finalproject.Apartament
import com.example.finalproject.data.ApartmentDao
import kotlinx.coroutines.launch

class ApartmentViewModel(private val dao: ApartmentDao) : ViewModel() {

    val allApartments: LiveData<List<Apartament>> = dao.getAll().asLiveData()

    fun addApartment(apartment: Apartament) = viewModelScope.launch {
        dao.insert(apartment)
    }

    fun updateApartment(apartment: Apartament) = viewModelScope.launch {
        dao.update(apartment)
    }

    fun deleteApartment(number: Int) = viewModelScope.launch {
        dao.deleteByNumber(number)
    }

    suspend fun findApp(number: Int): Boolean {
        return dao.findByNumber(number) != null
    }
}