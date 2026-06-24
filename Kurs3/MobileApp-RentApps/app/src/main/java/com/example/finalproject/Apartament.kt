package com.example.finalproject

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "apartments")
data class Apartament(
    @PrimaryKey
    var ApartamentNumb: Int,
    var Area: Double,
    var Rent: Int,
    var CntRooms: Int,
    var IsRented: Boolean
)
