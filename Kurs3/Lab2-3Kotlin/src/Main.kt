package app

fun main(){
    val dsl=apartments {
        add(Apartament(25000.0, Adress("Main Street", 101, 1), 55.0, 2, true))
        add(Apartament(30000.0, Adress("Second Street", 102, 1), 65.0, 3, false))
        add(Apartament(20000.0, Adress("Third Street", 103, 2), 45.0, 1, true))
        add(Apartament(40000.0, Adress("Fourth Street", 104, 2), 75.0, 4, false))
        add(Apartament(35000.0, Adress("Fifth Street", 105, 3), 70.0, 3, true))

        filter (by( "1" , PropertyCriteria.NUMB))

        sort ( by (SortCriteria.STRAIGHT, PropertyCriteria.CNTROOMS))

        (select(101))?.inc()

        (select(102))?.dec()

    }

}