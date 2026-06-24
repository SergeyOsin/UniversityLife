// ApartmentDao.kt
package com.example.finalproject.data

import androidx.room.*
import com.example.finalproject.Apartament
import kotlinx.coroutines.flow.Flow

@Dao
interface ApartmentDao {

    @Query("SELECT * FROM apartments")
    fun getAll(): Flow<List<Apartament>>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(apartment: Apartament)

    @Update
    suspend fun update(apartment: Apartament)

    @Delete
    suspend fun delete(apartment: Apartament)

    @Query("DELETE FROM apartments WHERE ApartamentNumb = :number")
    suspend fun deleteByNumber(number: Int)

    @Query("SELECT COUNT(*) FROM apartments WHERE ApartamentNumb = :number")
    suspend fun findCountByNumber(number: Int): Int

    @Query("SELECT * FROM apartments WHERE ApartamentNumb = :number LIMIT 1")
    suspend fun findByNumber(number: Int): Apartament?
}
