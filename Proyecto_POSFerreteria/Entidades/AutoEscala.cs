using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_POSFerreteria.Entidades
{
    public class AutoEscala
    {
        private Size originalFormSize;
        private Dictionary<Control, Rectangle> originalRects = new Dictionary<Control, Rectangle>();
        private Dictionary<Control, float> originalFontSizes = new Dictionary<Control, float>();
        private Dictionary<DataGridViewColumn, int> originalDgvColWidths = new Dictionary<DataGridViewColumn, int>();

        // Opciones
        public bool UseUniformScale { get; set; } = true; // true = mantiene proporción (min(scaleX,scaleY))
        public bool ScaleDgvColumns { get; set; } = true; // si tienes DataGridView
        public int MinFontSize = 6;

        public void Capture(Form form)
        {
            originalFormSize = form.Size;
            originalRects.Clear();
            originalFontSizes.Clear();
            originalDgvColWidths.Clear();

            Store(form);
            // Guarda columnas originales de DGVs
            var allDgvs = GetAllControls(form).OfType<DataGridView>();
            foreach (var dgv in allDgvs)
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    originalDgvColWidths[col] = col.Width;
                }
            }
        }

        private void Store(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                // Ignora controles creados dinámicamente sin tamaño válido
                if (c.Width > 0 && c.Height > 0)
                {
                    originalRects[c] = c.Bounds;
                    originalFontSizes[c] = c.Font?.Size ?? 8f;
                }

                if (c.HasChildren)
                    Store(c);
            }
        }

        public void Resize(Form form)
        {
            if (originalFormSize.Width == 0 || originalFormSize.Height == 0) return;

            float scaleX = (float)form.Width / originalFormSize.Width;
            float scaleY = (float)form.Height / originalFormSize.Height;
            float scaleUniform = Math.Min(scaleX, scaleY);

            // Elige escala
            float useScaleX = UseUniformScale ? scaleUniform : scaleX;
            float useScaleY = UseUniformScale ? scaleUniform : scaleY;

            // Evita parpadeo
            form.SuspendLayout();

            foreach (var kv in originalRects.ToList()) // ToList para evitar modificación en enumeración
            {
                Control c = kv.Key;
                if (c.IsDisposed) continue;

                Rectangle orig = kv.Value;

                int newX = (int)Math.Round(orig.X * useScaleX);
                int newY = (int)Math.Round(orig.Y * useScaleY);
                int newW = Math.Max(1, (int)Math.Round(orig.Width * useScaleX));
                int newH = Math.Max(1, (int)Math.Round(orig.Height * useScaleY));

                // Si el control está docked Fill y pertenece a un TableLayoutPanel/Panel, puedes saltarlo si te da problemas
                if (c.Dock == DockStyle.Fill && !(c is Label))
                {
                    // opcional: dejar que el Dock/admin del contenedor lo ajuste
                }
                else
                {
                    c.Bounds = new Rectangle(newX, newY, newW, newH);
                }

                // Escala fuente
                if (originalFontSizes.TryGetValue(c, out float origFontSize))
                {
                    float newFontSize = Math.Max(MinFontSize, origFontSize * (UseUniformScale ? useScaleX : Math.Min(useScaleX, useScaleY)));
                    try
                    {
                        c.Font = new Font(c.Font.FontFamily, newFontSize, c.Font.Style);
                    }
                    catch
                    {
                        // algunos controles no permiten cambiar la fuente de esa forma, ignorar
                    }
                }
            }

            // Ajusta ancho de columnas DataGridView si corresponde
            if (ScaleDgvColumns)
            {
                foreach (var pair in originalDgvColWidths)
                {
                    var col = pair.Key;
                    if (col != null && !col.DataGridView.IsDisposed)
                    {
                        float newW = pair.Value * (UseUniformScale ? useScaleX : useScaleX);
                        col.Width = Math.Max(20, (int)Math.Round(newW));
                    }
                }
            }

            form.ResumeLayout();
        }

        // helper recursivo
        private IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                yield return c;
                if (c.HasChildren)
                {
                    foreach (var cc in GetAllControls(c))
                        yield return cc;
                }
            }
        }
    }
}

