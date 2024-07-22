using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Quiz;

namespace api.Dtos.Chapitre
{
public class ChapitreDto
{
    public int Id { get; set; }
    public int ChapitreNum { get; set; }
    public string Nom { get; set; }
    public bool Statue { get; set; } = false;
    public string CoursPdfPath { get; set; }
    public string VideoPath { get; set; }
    public string Synthese { get; set; }
    public string Schema { get; set; }
    public bool Premium { get; set; }
    public QuizDto Quiz { get; set; }
}
}