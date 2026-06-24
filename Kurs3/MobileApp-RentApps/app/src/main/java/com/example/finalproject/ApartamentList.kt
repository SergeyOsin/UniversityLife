package com.example.finalproject

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView

class ApartamentList(
    private val onClick: (Apartament) -> Unit
) : ListAdapter<Apartament, ApartamentList.ViewHolder>(ApartmentDiffCallback()) {

    class ViewHolder(view: View, private val onClick: (Apartament) -> Unit) :
        RecyclerView.ViewHolder(view) {
        private val numb: TextView = view.findViewById(R.id.NumbApps)
        private val area: TextView = view.findViewById(R.id.Area)
        private val countRooms: TextView = view.findViewById(R.id.Rooms)
        private val coint: TextView = view.findViewById(R.id.Rent)
        private val status: TextView = view.findViewById(R.id.Status)

        private var currentApartment: Apartament? = null

        init {
            view.setOnClickListener { currentApartment?.let { onClick(it) } }
        }

        fun bind(apartament: Apartament) {
            currentApartment = apartament
            numb.text = apartament.ApartamentNumb.toString()
            status.text = if (apartament.IsRented) "Сдана" else "Несдана"
            area.text = "${apartament.Area} м2"
            coint.text = "${apartament.Rent} руб"
            countRooms.text = apartament.CntRooms.toString()
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.apps_item_layout, parent, false)
        return ViewHolder(view, onClick)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val apartment = getItem(position)
        holder.bind(apartment)
    }
}

class ApartmentDiffCallback : DiffUtil.ItemCallback<Apartament>() {
    override fun areItemsTheSame(oldItem: Apartament, newItem: Apartament): Boolean {
        return oldItem.ApartamentNumb == newItem.ApartamentNumb
    }

    override fun areContentsTheSame(oldItem: Apartament, newItem: Apartament): Boolean {
        return oldItem == newItem
    }
}
