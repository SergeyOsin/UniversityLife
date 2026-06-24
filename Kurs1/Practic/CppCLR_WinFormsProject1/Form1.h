#pragma once
#include "Figure.h"
#include "Myrectangle.h"
#include "Triangle.h"

namespace CppCLRWinFormsProject {
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Collections::Generic;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for Form1
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}

	protected:
	private: System::Windows::Forms::Button^ button2;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::Label^ label3;

	private: System::Windows::Forms::Button^ button4;

	private: System::Windows::Forms::Label^ label4;




	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::Label^ label6;

	private: System::Windows::Forms::MaskedTextBox^ maskedTextBox3;
	private: System::Windows::Forms::MaskedTextBox^ maskedTextBox4;
	private: System::Windows::Forms::TextBox^ textBox1;
	private: System::Windows::Forms::TextBox^ textBox2;

	private: System::Windows::Forms::ListBox^ listBox1;

	private: System::Windows::Forms::Button^ button3;
	private: System::Windows::Forms::Button^ button5;
	private: System::Windows::Forms::Button^ button6;
	private: System::Windows::Forms::MaskedTextBox^ maskedTextBox2;
	private: System::Windows::Forms::Button^ button7;
	private: System::Windows::Forms::Button^ button1;
	private: System::Windows::Forms::Label^ label7;

	private: System::Windows::Forms::ComboBox^ comboBox2;
	private: System::Windows::Forms::ComboBox^ comboBox1;
	private: System::Windows::Forms::Label^ label8;






	protected:

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container^ components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->button4 = (gcnew System::Windows::Forms::Button());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->maskedTextBox3 = (gcnew System::Windows::Forms::MaskedTextBox());
			this->maskedTextBox4 = (gcnew System::Windows::Forms::MaskedTextBox());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->textBox2 = (gcnew System::Windows::Forms::TextBox());
			this->listBox1 = (gcnew System::Windows::Forms::ListBox());
			this->button3 = (gcnew System::Windows::Forms::Button());
			this->button5 = (gcnew System::Windows::Forms::Button());
			this->button6 = (gcnew System::Windows::Forms::Button());
			this->maskedTextBox2 = (gcnew System::Windows::Forms::MaskedTextBox());
			this->button7 = (gcnew System::Windows::Forms::Button());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->comboBox2 = (gcnew System::Windows::Forms::ComboBox());
			this->comboBox1 = (gcnew System::Windows::Forms::ComboBox());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			// 
			// button2
			// 
			this->button2->Location = System::Drawing::Point(239, 409);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(130, 37);
			this->button2->TabIndex = 2;
			this->button2->Text = L"Скрыть";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->Click += gcnew System::EventHandler(this, &Form1::button2_Click);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(12, 345);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(20, 20);
			this->label1->TabIndex = 3;
			this->label1->Text = L"X";
			this->label1->Click += gcnew System::EventHandler(this, &Form1::label1_Click);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(12, 386);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(20, 20);
			this->label2->TabIndex = 4;
			this->label2->Text = L"Y";
			this->label2->Click += gcnew System::EventHandler(this, &Form1::label2_Click);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(12, 424);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(20, 20);
			this->label3->TabIndex = 5;
			this->label3->Text = L"A";
			this->label3->Click += gcnew System::EventHandler(this, &Form1::label3_Click);
			// 
			// button4
			// 
			this->button4->Location = System::Drawing::Point(394, 402);
			this->button4->Name = L"button4";
			this->button4->Size = System::Drawing::Size(128, 40);
			this->button4->TabIndex = 7;
			this->button4->Text = L"Переместить ";
			this->button4->UseVisualStyleBackColor = true;
			this->button4->Click += gcnew System::EventHandler(this, &Form1::button4_Click);
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(34, 193);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(120, 20);
			this->label4->TabIndex = 9;
			this->label4->Text = L"Выбор фигуры";
			this->label4->Click += gcnew System::EventHandler(this, &Form1::label4_Click);
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(562, 383);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(20, 20);
			this->label5->TabIndex = 14;
			this->label5->Text = L"X";
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(625, 383);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(20, 20);
			this->label6->TabIndex = 15;
			this->label6->Text = L"Y";
			// 
			// maskedTextBox3
			// 
			this->maskedTextBox3->Location = System::Drawing::Point(43, 383);
			this->maskedTextBox3->Name = L"maskedTextBox3";
			this->maskedTextBox3->Size = System::Drawing::Size(111, 26);
			this->maskedTextBox3->TabIndex = 18;
			// 
			// maskedTextBox4
			// 
			this->maskedTextBox4->Location = System::Drawing::Point(43, 421);
			this->maskedTextBox4->Name = L"maskedTextBox4";
			this->maskedTextBox4->Size = System::Drawing::Size(111, 26);
			this->maskedTextBox4->TabIndex = 19;
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(546, 409);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(49, 26);
			this->textBox1->TabIndex = 20;
			// 
			// textBox2
			// 
			this->textBox2->Location = System::Drawing::Point(612, 409);
			this->textBox2->Name = L"textBox2";
			this->textBox2->Size = System::Drawing::Size(52, 26);
			this->textBox2->TabIndex = 21;
			// 
			// listBox1
			// 
			this->listBox1->FormattingEnabled = true;
			this->listBox1->ItemHeight = 20;
			this->listBox1->Location = System::Drawing::Point(778, 48);
			this->listBox1->Name = L"listBox1";
			this->listBox1->Size = System::Drawing::Size(321, 244);
			this->listBox1->TabIndex = 22;
			this->listBox1->SelectedIndexChanged += gcnew System::EventHandler(this, &Form1::listBox1_SelectedIndexChanged);
			// 
			// button3
			// 
			this->button3->Location = System::Drawing::Point(801, 298);
			this->button3->Name = L"button3";
			this->button3->Size = System::Drawing::Size(282, 39);
			this->button3->TabIndex = 24;
			this->button3->Text = L"Поместить фигуру в контейнер";
			this->button3->UseVisualStyleBackColor = true;
			this->button3->Click += gcnew System::EventHandler(this, &Form1::button3_Click_1);
			// 
			// button5
			// 
			this->button5->Location = System::Drawing::Point(801, 345);
			this->button5->Name = L"button5";
			this->button5->Size = System::Drawing::Size(282, 35);
			this->button5->TabIndex = 25;
			this->button5->Text = L"Показать фигуру из контейнера";
			this->button5->UseVisualStyleBackColor = true;
			this->button5->Click += gcnew System::EventHandler(this, &Form1::button5_Click);
			// 
			// button6
			// 
			this->button6->Location = System::Drawing::Point(801, 386);
			this->button6->Name = L"button6";
			this->button6->Size = System::Drawing::Size(282, 30);
			this->button6->TabIndex = 26;
			this->button6->Text = L"Очистить контейнер";
			this->button6->UseVisualStyleBackColor = true;
			this->button6->Click += gcnew System::EventHandler(this, &Form1::button6_Click);
			// 
			// maskedTextBox2
			// 
			this->maskedTextBox2->Location = System::Drawing::Point(43, 342);
			this->maskedTextBox2->Name = L"maskedTextBox2";
			this->maskedTextBox2->Size = System::Drawing::Size(111, 26);
			this->maskedTextBox2->TabIndex = 17;
			this->maskedTextBox2->MaskInputRejected += gcnew System::Windows::Forms::MaskInputRejectedEventHandler(this, &Form1::maskedTextBox2_MaskInputRejected);
			// 
			// button7
			// 
			this->button7->Location = System::Drawing::Point(987, 421);
			this->button7->Name = L"button7";
			this->button7->Size = System::Drawing::Size(112, 39);
			this->button7->TabIndex = 27;
			this->button7->Text = L"Выход";
			this->button7->UseVisualStyleBackColor = true;
			this->button7->Click += gcnew System::EventHandler(this, &Form1::button7_Click);
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(239, 345);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(130, 45);
			this->button1->TabIndex = 28;
			this->button1->Text = L"Нарисовать";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &Form1::button1_Click);
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(878, 25);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(114, 20);
			this->label7->TabIndex = 29;
			this->label7->Text = L"Список фигур";
			// 
			// comboBox2
			// 
			this->comboBox2->FormattingEnabled = true;
			this->comboBox2->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"Red", L"Blue", L"Purple" });
			this->comboBox2->Location = System::Drawing::Point(43, 287);
			this->comboBox2->Name = L"comboBox2";
			this->comboBox2->Size = System::Drawing::Size(99, 28);
			this->comboBox2->TabIndex = 31;
			// 
			// comboBox1
			// 
			this->comboBox1->FormattingEnabled = true;
			this->comboBox1->Items->AddRange(gcnew cli::array< System::Object^  >(3) { L"Сложная Фигура", L"Прямоугольник", L"Треугольник" });
			this->comboBox1->Location = System::Drawing::Point(12, 225);
			this->comboBox1->Name = L"comboBox1";
			this->comboBox1->Size = System::Drawing::Size(170, 28);
			this->comboBox1->TabIndex = 8;
			this->comboBox1->SelectedIndexChanged += gcnew System::EventHandler(this, &Form1::comboBox1_SelectedIndexChanged);
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Location = System::Drawing::Point(39, 264);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(110, 20);
			this->label8->TabIndex = 32;
			this->label8->Text = L"Цвет фигуры";
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(9, 20);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(1111, 472);
			this->Controls->Add(this->label8);
			this->Controls->Add(this->comboBox2);
			this->Controls->Add(this->label7);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->button7);
			this->Controls->Add(this->button6);
			this->Controls->Add(this->button5);
			this->Controls->Add(this->button3);
			this->Controls->Add(this->listBox1);
			this->Controls->Add(this->textBox2);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->maskedTextBox4);
			this->Controls->Add(this->maskedTextBox3);
			this->Controls->Add(this->maskedTextBox2);
			this->Controls->Add(this->label6);
			this->Controls->Add(this->label5);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->comboBox1);
			this->Controls->Add(this->button4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->button2);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->Name = L"Form1";
			this->Text = L"23VP2_18";
			this->Load += gcnew System::EventHandler(this, &Form1::Form1_Load);
			this->ResumeLayout(false);
			this->PerformLayout();
		}
#pragma endregion

	private: System::Void button2_Click(System::Object^ sender, System::EventArgs^ e) {
		try {
			Graphics^ g = CreateGraphics();
			if (maskedTextBox2->Text == "" || maskedTextBox3->Text == "" || maskedTextBox4->Text == "")
				throw gcnew System::Exception("Напишите координаты");
			if (comboBox1->SelectedIndex == -1)
				throw gcnew System::Exception("Выберите фигуру");
			int x1 = Convert::ToInt32(maskedTextBox2->Text);
			int y1 = Convert::ToInt32(maskedTextBox3->Text);
			int a1 = Convert::ToInt32(maskedTextBox4->Text);
			Triangle^ t = gcnew Triangle(x1, y1, a1, Color::White);
			MyRectangle^ r = gcnew MyRectangle(x1, y1, a1, Color::White,Color::White);
			if (comboBox1->SelectedIndex == 2) t->Hide(g);
			if (comboBox1->SelectedIndex == 1) r->Hide(g);
			if (comboBox1->SelectedIndex == 0) {
				t->Hide(g);
				r->Hide(g);
			}
		}
		catch (System::Exception^ ex) {
			MessageBox::Show(this, ex->Message, "Ошибка", MessageBoxButtons::OK, MessageBoxIcon::Error);
		}
	}
	private: System::Void label1_Click(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void label2_Click(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void label3_Click(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void Form1_Load(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void button4_Click(System::Object^ sender, System::EventArgs^ e) {
		try {
			Graphics^ g = CreateGraphics();
			Dictionary<String^, Color>^ colors;
			if (maskedTextBox2->Text == "" || maskedTextBox3->Text == "" || maskedTextBox4->Text == "")
				throw gcnew System::Exception("Напишите координаты");
			if (comboBox1->SelectedIndex == -1)
				throw gcnew System::Exception("Выберите фигуру");
			int x1 = Convert::ToInt32(maskedTextBox2->Text);
			int y1 = Convert::ToInt32(maskedTextBox3->Text);
			int a1 = Convert::ToInt32(maskedTextBox4->Text);
			if (textBox1->Text == "" || textBox2->Text == "")
				throw gcnew System::Exception("Напишите координаты перемещения");
			int n_x = Convert::ToInt32(textBox1->Text);
			int n_y = Convert::ToInt32(textBox2->Text);
			Triangle^ t = gcnew Triangle(x1, y1, a1, Color::Black);
			MyRectangle^ r = gcnew MyRectangle(x1, y1, a1, Color::Green, Color::Green);
			if (comboBox1->SelectedIndex == 2) t->Move(n_x, n_y, g);
			if (comboBox1->SelectedIndex == 1) r->Move(n_x, n_y, g);
			if (comboBox1->SelectedIndex == 0) {
				t->Move(n_x, n_y, g);
				r->Move(n_x, n_y, g);
			}
		}
		catch (System::Exception^ ex) {
			MessageBox::Show(this, ex->Message, "Ошибка", MessageBoxButtons::OK, MessageBoxIcon::Error);
		}
	}
	private: System::Void comboBox1_SelectedIndexChanged(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void label4_Click(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void button3_Click(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void maskedTextBox2_MaskInputRejected(System::Object^ sender, System::Windows::Forms::MaskInputRejectedEventArgs^ e) {

	}
	private: System::Void listBox1_SelectedIndexChanged(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void button3_Click_1(System::Object^ sender, System::EventArgs^ e) {
		try {
			Graphics^ g = CreateGraphics();
			if (maskedTextBox2->Text == "" || maskedTextBox3->Text == "" || maskedTextBox4->Text == "")
				throw gcnew System::Exception("Напишите координаты");
			if (comboBox1->SelectedIndex == -1)
				throw gcnew System::Exception("Выберите фигуру");
			int x1 = Convert::ToInt32(maskedTextBox2->Text);
			int y1 = Convert::ToInt32(maskedTextBox3->Text);
			int a1 = Convert::ToInt32(maskedTextBox4->Text);
			String^ tr = comboBox1->SelectedIndex + " (" + x1 + "," + y1 + "," + a1;
			if (comboBox1->SelectedIndex == 2) {
				listBox1->Items->Add("Треугольник (" + x1 + "," + y1 + "," + a1 + " )");
			}
			if (comboBox1->SelectedIndex == 1) {
				listBox1->Items->Add("Прямоугольник (" + x1 + "," + y1 + "," + a1 + " )");
			}
			if (comboBox1->SelectedIndex == 0) {
				listBox1->Items->Add("Сложная фигура (" + x1 + " " + y1 + " " + a1 + " )");
			}
		}
		catch (System::Exception^ d) {
			MessageBox::Show(this, d->Message, "Ошибка", MessageBoxButtons::OK, MessageBoxIcon::Error);
		}
	}
	private: System::Void button5_Click(System::Object^ sender, System::EventArgs^ e) {
		try {
			Graphics^ g = CreateGraphics();
			int x1 = Convert::ToInt32(maskedTextBox2->Text);
			int y1 = Convert::ToInt32(maskedTextBox3->Text);
			int a1 = Convert::ToInt32(maskedTextBox4->Text);
			String^ str = listBox1->Items[listBox1->SelectedIndex]->ToString();
			Triangle^ t = gcnew Triangle(x1, y1, a1, Color::Black);
			MyRectangle^ r = gcnew MyRectangle(x1, y1, a1, Color::Black, Color::Green);
			int spacePos = str->IndexOf(" ");
			String^ firstWord = str->Substring(0, spacePos);
			if (firstWord == "Треугольник") {
				t->Draw(g);
			}
			if (firstWord == "Прямоугольник") r->Draw(g);
			if (firstWord == "Сложная") {
				t->Draw(g);
				r->Draw(g);
			}
		}
		catch (System::Exception^ d) {
			MessageBox::Show(this, d->Message, "Ошибка", MessageBoxButtons::OK, MessageBoxIcon::Error);
		}
	}
	private: System::Void button6_Click(System::Object^ sender, System::EventArgs^ e) {
		listBox1->Items->Clear();
	}

	private: System::Void button7_Click(System::Object^ sender, System::EventArgs^ e) {
		Close();
	}
	private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e) {
		try {
			Graphics^ g = CreateGraphics();
			Rectangle rect = Form::ClientRectangle;
			if (maskedTextBox2->Text == "" || maskedTextBox3->Text == "" || maskedTextBox4->Text == "")
				throw gcnew System::Exception("Напишите координаты и длину сторону!");
			if (comboBox1->SelectedIndex == -1)
				throw gcnew System::Exception("Выберите фигуру!");
			int x1 = Convert::ToInt32(maskedTextBox2->Text);
			int y1 = Convert::ToInt32(maskedTextBox3->Text);
			int a1 = Convert::ToInt32(maskedTextBox4->Text);
			this->comboBox2->Items->AddRange(gcnew cli::array<System::Object^>(4) { L"Red", L"Blue", L"Green", L"Purple" });
			Dictionary<String^, Color>^ colors = gcnew Dictionary<String^, Color>();
			colors->Add("Red", Color::Red);
			colors->Add("Blue", Color::Blue);
			colors->Add("Purple", Color::Purple);
			if (comboBox1->SelectedIndex == 2) {
				Triangle^ t = gcnew Triangle(x1, y1, a1, colors[this->comboBox2->Text]);
				t->Draw(g);
			}
			if (comboBox1->SelectedIndex == 1) {
				MyRectangle^ r = gcnew MyRectangle(x1, y1, a1, colors[this->comboBox2->Text], colors[this->comboBox2->Text]);
				r->Draw(g);
			}
			if (comboBox1->SelectedIndex == 0) {
				Triangle^ t = gcnew Triangle(x1, y1, a1, colors[this->comboBox2->Text]);
				MyRectangle^ r = gcnew MyRectangle(x1, y1, a1, colors[this->comboBox2->Text], colors[this->comboBox2->Text]);
				t->Draw(g);
				r->Draw(g);
			}
			colors->Clear();
		}
		catch (System::Exception^ d) {
			MessageBox::Show(this, d->Message, "Ошибка", MessageBoxButtons::OK, MessageBoxIcon::Error);
		}
}
};
	}

	
