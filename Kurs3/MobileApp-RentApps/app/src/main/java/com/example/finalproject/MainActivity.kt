// MainActivity.kt
package com.example.finalproject

import android.os.Bundle
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.ui.setupWithNavController
import com.example.finalproject.data.ApartmentDao
import com.example.finalproject.data.AppDatabase
import com.example.finalproject.viewmodel.DataBaseViewModelFactory
import com.google.android.material.bottomnavigation.BottomNavigationView

class MainActivity : AppCompatActivity() {

    private lateinit var database: AppDatabase
    private lateinit var dao: ApartmentDao
    lateinit var viewModelFactory: DataBaseViewModelFactory

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContentView(R.layout.activity_main)

        database = AppDatabase.getDatabase(this)
        dao = database.apartmentDao()
        viewModelFactory = DataBaseViewModelFactory(dao)

        val butnav = findViewById<BottomNavigationView>(R.id.butnav1)
        val navHostFragment = supportFragmentManager
            .findFragmentById(R.id.fragmentContainerView) as NavHostFragment
        val controller = navHostFragment.navController
        butnav.setupWithNavController(controller)
    }
}
