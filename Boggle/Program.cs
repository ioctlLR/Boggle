using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boggle
{
	[SimpleJob]
	public class Program
	{
		private static readonly List<Core.SolverBase> s_solvers;

		static Program()
		{
			s_solvers = GetSolvers();
		}

		private static List<Core.SolverBase> GetSolvers()
		{
			var assembly = System.Reflection.Assembly.Load("Boggle.Solvers");
			var solverTypes = assembly.GetTypes().Where(t => t.BaseType == typeof(Core.SolverBase));
			var solvers = new List<Core.SolverBase>();
			foreach (var type in solverTypes)
			{
				solvers.Add((Core.SolverBase)Activator.CreateInstance(type));
			}
			return solvers;
		}

		private static bool VerifySolvers(List<Core.SolverBase> solvers, string board, IEnumerable<string> dictionary, Core.Scorer scorer, int expectedWords, int expectedScore)
		{
			var boardSize = (int)Math.Sqrt(board.Length);
			var success = true;
			foreach (var solver in solvers)
			{
				solver.Init(dictionary, boardSize);
				var (words, score) = scorer.Score(solver.Solve(board));
				if (words != expectedWords)
				{
					Console.WriteLine($"{solver.GetType().Name} found {words} words, but we expected {expectedWords}.");
					success = false;
					continue;
				}
				if (score != expectedScore)
				{
					Console.WriteLine($"{solver.GetType().Name} scored {score}, but we expected {expectedScore}.");
					success = false;
					continue;
				}
			}
			return success;
		}

		static void Main(string[] args)
		{
			var dictionary = Core.WordListProvider.GetWordList();
			var scorer = new Core.Scorer(dictionary);

			if (!VerifySolvers(s_solvers, "ritqwtagsceopkyr", dictionary, scorer, 194, 335)) return;
			if (!VerifySolvers(s_solvers, "gnessripetaltseb", dictionary, scorer, 1351, 4540)) return;

#if !DEBUG
			Console.WriteLine("All solvers passed.  Starting Benchmarks...");

			BenchmarkRunner.Run<Program>();
#endif

			foreach (var solver in s_solvers)
			{
				(solver as IDisposable)?.Dispose();
			}
		}

		public IEnumerable<Core.SolverBase> Solvers => s_solvers;

		// our two known standard boards, then a 10x10 and a 50x50 non-standard boards
		[Params("ritqwtagsceopkyr", "gnessripetaltseb", "zfonsdlrsooaoetpumbeoixerkryahcnpanthclntawthilystarvmndebaeesoistvteeityiheugritjolqdfeewgnwuhsoerk", "oniqtoulyztwasexiyilgsmrcuijadtenlpavsoawesneinrrshtgkwtdvrenuethohaoenhrafcbofdebolmtphiseyetteewtafrenmhgieaotebilneedenewvlyuhtaafpolporsotsicnjgbainhulsesqihetnutdsrtohyxyzcoidktmvesarowtrcjtlehetpdefrcseoagiznmtphoewoletityytwefrataloiuoedeotintvbouayesvbsnshsqwsairghedmlxrinarnnuhkvaaaseodehxefmnrtteulhtlinqophdiuctogrtspbbyteirehanweaaiztogswshyyikcetrnweerilomuodslvnfnstejoooduedorytrupnztttwiageetkhnielomoshvdwnehrraweabfpehleigvsstatoistxisamlloyycntecneqnfrhubijesaiooegtepinmoedceslnehsdxasyimubnradwvopethlvuhurjntstefratzwrctgahnohtiqeolybiietwynalrsatkeoefstrablyhysnlcteaoixizldteeyivnuemtetetafqavormfnwkhcgrseoetethwnonbiiwhlstdoasiuhsrugaodejnrposeptetqhstfuhiewsoounisenpahdrtciauoettistlerimxglzjiesnnwbodglvthyrtrbmdcoenaaehfrevyasnoloewykeapoenqbouvxnezrgmsslsceeegyvjuitroyceiarplthletfiouniwniraasienttkeotdbrspndmohaltwwhfehatydaethossntrnyharsxflecmusosskynsyhdjdwwitilprhetelbeeeuogetgtbciiaufeioteraaqenhhnparveotmzltntdwiovooatorsdyywmcfiutratiwexagldnravsbotjannzheieeuwgaulernoqheptthombiestsfseoethsokvcneyirnladtiehplohvfseekatcworytsirovnyseethwnzuttiegwmalonyoaorrpletuishnqbrgftntaeddtlinalpujbsaehhxeicdsoeiemoinirsltdtebeouidhhlodcvahewsnttaeemyroweeonlkrsehioeqvuigotepjrntiyeapnanrslbauhfottftmygxzawscsrrsgcdafwtuyaiqyazxhsroinlsnesnpcllonoiyiiabusieetphreemaltnejhthuvvtweeoaoeetgrowesdknbhmtfdtotkgobteevaafoishnyhmhwyipepwluonutssacetugqlvlbtozfltcymtedrwdnjtenreeornaiahaxsoiieseodirtsnthernygcsseluttzvefsishppuoimenihneikbndoaoearuwottlaexlthreaeedoesiroahvhnnibtsgtqrmltdayjwocwteyrftefieoethietavzngprgwoarvjeedotbnqsmadutyonukhbwlrlcataeohotriwtrsnmsyectsepnfiisehnuoxdyahleslivhrasraoomptjteyetdrotstluqmeekntiisttnaiueodyaplvitgxrnwlhocusczfhbarweogifoihwseenaeesdehylnnboldsifgubypotghulhlcnaoewymloeiwvyfatnvusqextttidshwemitaodrzsbranipierneaerosanorheejtctsnehetkdettfipeqhhonastwylawfekzovgmneichensevltynrssroomchreleadgextaonpdiuouejytbisrelowsbnitatuihratarafennqavbclfoeedtaedtpgtnsirhstouaswnaiolinohtilltieeerjsybnkerzpdtemevythshiyhwxtewougrocmosuitrtvzswcplaeefoqvatyepatyasrnhdwenlraxoeuogrtshtenssiutetlhkooynetehidoecbehiadmsfiuwmnborjinglhesdidwqrctniltelomansbaohtiierevnpsieesynbujleryfvaarzelohotwncoeimtxtofastuhdpogsreuytgwthknearazdcimeuliyrxgtolndeofeamorsaatwfusrdetgehletiieshweatrheaepvjnpnhoisykisqovnoonusbtteclytbwtnhootragfeftentimehtnntyzouexlnttuoeiuriaaetdosbkierysheoopgidcmahhnivjdsbytrlcsewavaeswlwrqelnphsektaaofcgnxiawatfsjurthpnhtetoorerlisybvssiyszqnpecwnstauwilegreyleitvltmoiubdodhedotheeonnrmhaexooe")]
		public string Board { get; set; }

		[ParamsSource(nameof(Solvers))]
		public Core.SolverBase Solver { get; set; }

		[GlobalSetup]
		public void Setup()
		{
			var boardSize = (int)Math.Sqrt(Board.Length);

			Solver.Init(Core.WordListProvider.GetWordList(), boardSize);
			Solver.Solve(Board);
		}

		[Benchmark]
		public void Run()
		{
			Solver.Reset();
			Solver.Solve(Board).Count();
		}
	}
}
