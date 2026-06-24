package app
data class Apartament(var Rent: Double, var adress: Adress, var Area: Double, var CntRooms: Int,
    var IsRented: Boolean) {

    operator fun plus(other: Double): Apartament {
        return Apartament(this.Rent + other, this.adress, this.Area, this.CntRooms, this.IsRented)
    }

    operator fun minus(other: Double): Apartament {
        if (this.Rent -  other < 0) {return this}
        return Apartament(this.Rent + other, this.adress, this.Area, this.CntRooms, this.IsRented)
    }

    operator fun inc(): Apartament {
        return this + 1.0
    }

    operator fun dec(): Apartament {
        return this - 1.0;
    }

    override fun toString(): String {
        return "Квартира №${adress.ApartmentNumb} | " +
                "Площадь: $Area м² | " +
                "Комнат: $CntRooms | " +
                "Аренда: $Rent руб/мес | " +
                "Статус: ${if (IsRented) "Сдана" else "Свободна"}"
    }

}

data class Adress(var Street: String, var ApartmentNumb: Int, var HouseNumb: Int){}
