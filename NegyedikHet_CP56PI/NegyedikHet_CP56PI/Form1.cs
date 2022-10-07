﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace NegyedikHet_CP56PI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> flats;

        private void LoadData()
        {
            flats = context.Flat.ToList();
        }
    }
}
