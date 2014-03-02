#pragma once
#include "stdafx.h"

namespace ItemFilter {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for MainWindow
	/// </summary>
	public ref class MainWindow : public System::Windows::Forms::Form
	{
	public:
		MainWindow(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
			ifstream filter("filter.txt");
			if (filter.is_open())
			{
				while (filter.good())
				{
					char buffer[16];
					filter.getline(buffer, 16);
					int itemid = atoi(buffer);
					FilterList.push_back(itemid);
					this->listBox1->Items->Add(itemid.ToString());
				}
				UpdateFilter();
				filter.close();
			}
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~MainWindow()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::ListBox^  listBox1;
	protected: 


	private: System::Windows::Forms::TextBox^  textBox1;
	private: System::Windows::Forms::CheckBox^  checkBox1;
	private: System::Windows::Forms::CheckBox^  checkBox2;
	private: System::Windows::Forms::CheckBox^  checkBox3;
	private: System::Windows::Forms::TextBox^  textBox2;
	private: System::Windows::Forms::Button^  button2;
	private: System::Windows::Forms::Button^  button1;
	private: System::Windows::Forms::CheckBox^  checkBox4;


	private: System::Windows::Forms::CheckBox^  checkBox6;
	private: System::Windows::Forms::CheckBox^  checkBox7;
	private: System::Windows::Forms::CheckBox^  checkBox8;


	private: System::Windows::Forms::CheckBox^  checkBox10;
	private: System::Windows::Forms::CheckBox^  checkBox11;
	private: System::Windows::Forms::Timer^  NDTimer;



	private: System::ComponentModel::IContainer^  components;

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>


#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->components = (gcnew System::ComponentModel::Container());
			this->listBox1 = (gcnew System::Windows::Forms::ListBox());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->checkBox1 = (gcnew System::Windows::Forms::CheckBox());
			this->checkBox2 = (gcnew System::Windows::Forms::CheckBox());
			this->checkBox3 = (gcnew System::Windows::Forms::CheckBox());
			this->textBox2 = (gcnew System::Windows::Forms::TextBox());
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->checkBox4 = (gcnew System::Windows::Forms::CheckBox());
			this->checkBox6 = (gcnew System::Windows::Forms::CheckBox());
			this->checkBox7 = (gcnew System::Windows::Forms::CheckBox());
			this->checkBox8 = (gcnew System::Windows::Forms::CheckBox());
			this->checkBox10 = (gcnew System::Windows::Forms::CheckBox());
			this->checkBox11 = (gcnew System::Windows::Forms::CheckBox());
			this->NDTimer = (gcnew System::Windows::Forms::Timer(this->components));
			this->SuspendLayout();
			// 
			// listBox1
			// 
			this->listBox1->FormattingEnabled = true;
			this->listBox1->Location = System::Drawing::Point(12, 12);
			this->listBox1->Name = L"listBox1";
			this->listBox1->Size = System::Drawing::Size(80, 121);
			this->listBox1->TabIndex = 0;
			this->listBox1->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &MainWindow::listBox1_KeyDown);
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(98, 58);
			this->textBox1->MaxLength = 9;
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(75, 20);
			this->textBox1->TabIndex = 3;
			this->textBox1->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &MainWindow::textBox1_KeyDown);
			// 
			// checkBox1
			// 
			this->checkBox1->AutoSize = true;
			this->checkBox1->Location = System::Drawing::Point(98, 12);
			this->checkBox1->Name = L"checkBox1";
			this->checkBox1->Size = System::Drawing::Size(71, 17);
			this->checkBox1->TabIndex = 4;
			this->checkBox1->Text = L"Item Filter";
			this->checkBox1->UseVisualStyleBackColor = true;
			this->checkBox1->CheckedChanged += gcnew System::EventHandler(this, &MainWindow::checkBox1_CheckedChanged);
			// 
			// checkBox2
			// 
			this->checkBox2->AutoSize = true;
			this->checkBox2->Location = System::Drawing::Point(98, 35);
			this->checkBox2->Name = L"checkBox2";
			this->checkBox2->Size = System::Drawing::Size(47, 17);
			this->checkBox2->TabIndex = 5;
			this->checkBox2->Text = L"Tubi";
			this->checkBox2->UseVisualStyleBackColor = true;
			this->checkBox2->CheckedChanged += gcnew System::EventHandler(this, &MainWindow::checkBox2_CheckedChanged);
			// 
			// checkBox3
			// 
			this->checkBox3->AutoSize = true;
			this->checkBox3->Location = System::Drawing::Point(98, 142);
			this->checkBox3->Name = L"checkBox3";
			this->checkBox3->Size = System::Drawing::Size(77, 17);
			this->checkBox3->TabIndex = 6;
			this->checkBox3->Text = L"Meso Filter";
			this->checkBox3->UseVisualStyleBackColor = true;
			this->checkBox3->CheckedChanged += gcnew System::EventHandler(this, &MainWindow::checkBox3_CheckedChanged);
			// 
			// textBox2
			// 
			this->textBox2->Location = System::Drawing::Point(98, 163);
			this->textBox2->MaxLength = 9;
			this->textBox2->Name = L"textBox2";
			this->textBox2->Size = System::Drawing::Size(75, 20);
			this->textBox2->TabIndex = 7;
			this->textBox2->Text = L"1000";
			// 
			// button2
			// 
			this->button2->Location = System::Drawing::Point(98, 113);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(75, 23);
			this->button2->TabIndex = 2;
			this->button2->Text = L"Remove";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->Click += gcnew System::EventHandler(this, &MainWindow::button2_Click);
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(98, 84);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(75, 23);
			this->button1->TabIndex = 1;
			this->button1->Text = L"Add";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MainWindow::button1_Click);
			// 
			// checkBox4
			// 
			this->checkBox4->AutoSize = true;
			this->checkBox4->Location = System::Drawing::Point(12, 142);
			this->checkBox4->Name = L"checkBox4";
			this->checkBox4->Size = System::Drawing::Size(73, 17);
			this->checkBox4->TabIndex = 8;
			this->checkBox4->Text = L"Kami Loot";
			this->checkBox4->UseVisualStyleBackColor = true;
			this->checkBox4->CheckedChanged += gcnew System::EventHandler(this, &MainWindow::checkBox4_CheckedChanged);
			// 
			// checkBox6
			// 
			this->checkBox6->AutoSize = true;
			this->checkBox6->Location = System::Drawing::Point(12, 188);
			this->checkBox6->Name = L"checkBox6";
			this->checkBox6->Size = System::Drawing::Size(50, 17);
			this->checkBox6->TabIndex = 10;
			this->checkBox6->Text = L"GND";
			this->checkBox6->UseVisualStyleBackColor = true;
			this->checkBox6->CheckedChanged += gcnew System::EventHandler(this, &MainWindow::checkBox6_CheckedChanged);
			// 
			// checkBox7
			// 
			this->checkBox7->AutoSize = true;
			this->checkBox7->Location = System::Drawing::Point(98, 188);
			this->checkBox7->Name = L"checkBox7";
			this->checkBox7->Size = System::Drawing::Size(49, 17);
			this->checkBox7->TabIndex = 11;
			this->checkBox7->Text = L"FGM";
			this->checkBox7->UseVisualStyleBackColor = true;
			this->checkBox7->CheckedChanged += gcnew System::EventHandler(this, &MainWindow::checkBox7_CheckedChanged);
			// 
			// checkBox8
			// 
			this->checkBox8->AutoSize = true;
			this->checkBox8->Enabled = false;
			this->checkBox8->Location = System::Drawing::Point(12, 165);
			this->checkBox8->Name = L"checkBox8";
			this->checkBox8->Size = System::Drawing::Size(76, 17);
			this->checkBox8->TabIndex = 12;
			this->checkBox8->Text = L"ND Mining";
			this->checkBox8->UseVisualStyleBackColor = true;
			this->checkBox8->CheckedChanged += gcnew System::EventHandler(this, &MainWindow::checkBox8_CheckedChanged);
			// 
			// checkBox10
			// 
			this->checkBox10->AutoSize = true;
			this->checkBox10->Location = System::Drawing::Point(10, 211);
			this->checkBox10->Name = L"checkBox10";
			this->checkBox10->Size = System::Drawing::Size(82, 17);
			this->checkBox10->TabIndex = 14;
			this->checkBox10->Text = L"Mob Freeze";
			this->checkBox10->UseVisualStyleBackColor = true;
			this->checkBox10->CheckedChanged += gcnew System::EventHandler(this, &MainWindow::checkBox10_CheckedChanged);
			// 
			// checkBox11
			// 
			this->checkBox11->AutoSize = true;
			this->checkBox11->Location = System::Drawing::Point(98, 211);
			this->checkBox11->Name = L"checkBox11";
			this->checkBox11->Size = System::Drawing::Size(71, 17);
			this->checkBox11->TabIndex = 15;
			this->checkBox11->Text = L"No Aggro";
			this->checkBox11->UseVisualStyleBackColor = true;
			this->checkBox11->CheckedChanged += gcnew System::EventHandler(this, &MainWindow::checkBox11_CheckedChanged);
			// 
			// NDTimer
			// 
			this->NDTimer->Interval = 10;
			this->NDTimer->Tick += gcnew System::EventHandler(this, &MainWindow::NDTimer_Tick);
			// 
			// MainWindow
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(183, 234);
			this->Controls->Add(this->checkBox11);
			this->Controls->Add(this->checkBox10);
			this->Controls->Add(this->checkBox8);
			this->Controls->Add(this->checkBox7);
			this->Controls->Add(this->checkBox6);
			this->Controls->Add(this->checkBox4);
			this->Controls->Add(this->textBox2);
			this->Controls->Add(this->checkBox3);
			this->Controls->Add(this->checkBox2);
			this->Controls->Add(this->checkBox1);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->button2);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->listBox1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedDialog;
			this->Name = L"MainWindow";
			this->Text = L"Item Filter";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
private: 
	System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) 
	{
		if(this->textBox1->TextLength > 0)
		{
			int item;
			if(int::TryParse(this->textBox1->Text,item))
			{
				if (item > 0 && !this->listBox1->Items->Contains(item.ToString()))
				{
					FilterList.push_back(item);
					UpdateFilter();
					this->listBox1->Items->Add(item.ToString());
					this->textBox1->Text = "";
					this->listBox1->SelectedIndex = this->listBox1->Items->Count - 1;
					this->listBox1->SelectedIndex = -1;
				}
			}
		}
	}
private: 
	System::Void textBox1_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e) 
	{
		if (e->KeyCode == Keys::Enter)
			this->button1_Click(nullptr, nullptr);
	}
private: 
	System::Void button2_Click(System::Object^  sender, System::EventArgs^  e) 
	{
		if(this->listBox1->SelectedItem != nullptr)
		{
			int index = this->listBox1->Items->IndexOf(this->listBox1->SelectedItem);
			this->listBox1->Items->RemoveAt(index);
			FilterList.erase(FilterList.begin() + index);
			UpdateFilter();
			this->listBox1->SelectedIndex = this->listBox1->Items->Count - 1;
			this->listBox1->SelectedIndex = -1;
		}
	}
private: 
	System::Void checkBox1_CheckedChanged(System::Object^  sender, System::EventArgs^  e) 
	{
		itemfilter = this->checkBox1->Checked;
		TogglePacket();
	}
private: 
	System::Void checkBox2_CheckedChanged(System::Object^  sender, System::EventArgs^  e) 
	{
		tubi = this->checkBox2->Checked;
		NDTimer->Enabled = tubi;
	}
private: 
	System::Void checkBox3_CheckedChanged(System::Object^  sender, System::EventArgs^  e) 
	{
		mesofilter = this->checkBox3->Checked;
		if (mesofilter)
		{
			if(int::TryParse(this->textBox2->Text, Mesos))
			{
				if(Mesos >= 10)
					this->textBox2->Enabled = false;
			}
			else
			{ 
				mesofilter = false; 
				this->checkBox3->Checked = false; 
			}
		}
		else
			this->textBox2->Enabled = true;
		TogglePacket();
	}
private: 
	System::Void listBox1_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e) 
	{
		if (Control::ModifierKeys == Keys::Control)
		{
			if (e->KeyCode == Keys::R)
				this->button2_Click(nullptr, nullptr);
		}
	}
private: 
	System::Void checkBox4_CheckedChanged(System::Object^  sender, System::EventArgs^  e) 
	{
		kamiloot = this->checkBox4->Checked;
	}
private: 
	System::Void checkBox6_CheckedChanged(System::Object^  sender, System::EventArgs^  e) 
	{
		gnd = this->checkBox6->Checked;
		ToggleGND();
		ToggleMC();
	}
private: 
	System::Void checkBox7_CheckedChanged(System::Object^  sender, System::EventArgs^  e) 
	{
		fgm = this->checkBox7->Checked;
		ToggleMC();
		ToggleMGM();
	}
private: 
	System::Void checkBox8_CheckedChanged(System::Object^  sender, System::EventArgs^  e) 
	{
		mining = this->checkBox8->Checked;
		TogglePacket();
	}
private: 
	System::Void checkBox10_CheckedChanged(System::Object^  sender, System::EventArgs^  e) 
	{
		 freeze = this->checkBox10->Checked;
		 ToggleMC();
	}
private: 
	System::Void checkBox11_CheckedChanged(System::Object^  sender, System::EventArgs^  e) 
	{
		 deaggro = this->checkBox11->Checked;
		 ToggleMC();
	}
private: 
	System::Void NDTimer_Tick(System::Object^  sender, System::EventArgs^  e) 
	{
		NoDelay();
	}
};
}
