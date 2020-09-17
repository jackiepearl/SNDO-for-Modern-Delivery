using Team1FinalProject.Models;
using Team1FinalProject.DAL;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Team1FinalProject.Seeding
{
	public static class SeedGenres
	{
		public static void SeedAllGenres(AppDbContext db)
		{
			if (db.Genres.Count() == 13)
			{
                NotSupportedException ex = new NotSupportedException("Genre record count is already 21!");
                throw ex;
            }

            Int32 intGenresAdded = 0;

			try
			{
                List<Genre> Genres = new List<Genre>();


                Genre g1 = new Genre();
				g1.GenreName = "Contemporary Fiction";
				Genres.Add(g1);

				Genre g2 = new Genre();
				g2.GenreName = "Science Fiction";
				Genres.Add(g2);

				Genre g3 = new Genre();
				g3.GenreName = "Mystery";
				Genres.Add(g3);

				Genre g4 = new Genre();
				g4.GenreName = "Suspense";
				Genres.Add(g4);

				Genre g5 = new Genre();
				g5.GenreName = "Romance";
				Genres.Add(g5);

				Genre g6 = new Genre();
				g6.GenreName = "Thriller";
				Genres.Add(g6);

				Genre g7 = new Genre();
				g7.GenreName = "Fantasy";
				Genres.Add(g7);

				Genre g8 = new Genre();
				g8.GenreName = "Contemporary Fiction";
				Genres.Add(g8);

				Genre g9 = new Genre();
				g9.GenreName = "Romance";
				Genres.Add(g9);

				Genre g10 = new Genre();
				g10.GenreName = "Fantasy";
				Genres.Add(g10);

				Genre g11 = new Genre();
				g11.GenreName = "Historical Fiction";
				Genres.Add(g11);

				Genre g12 = new Genre();
				g12.GenreName = "Romance";
				Genres.Add(g12);

				Genre g13 = new Genre();
				g13.GenreName = "Historical Fiction";
				Genres.Add(g13);

				Genre g14 = new Genre();
				g14.GenreName = "Romance";
				Genres.Add(g14);

				Genre g15 = new Genre();
				g15.GenreName = "Fantasy";
				Genres.Add(g15);

				Genre g16 = new Genre();
				g16.GenreName = "Mystery";
				Genres.Add(g16);

				Genre g17 = new Genre();
				g17.GenreName = "Mystery";
				Genres.Add(g17);

				Genre g18 = new Genre();
				g18.GenreName = "Suspense";
				Genres.Add(g18);

				Genre g19 = new Genre();
				g19.GenreName = "Romance";
				Genres.Add(g19);

				Genre g20 = new Genre();
				g20.GenreName = "Contemporary Fiction";
				Genres.Add(g20);

				Genre g21 = new Genre();
				g21.GenreName = "Mystery";
				Genres.Add(g21);

				Genre g22 = new Genre();
				g22.GenreName = "Suspense";
				Genres.Add(g22);

				Genre g23 = new Genre();
				g23.GenreName = "Mystery";
				Genres.Add(g23);

				Genre g24 = new Genre();
				g24.GenreName = "Fantasy";
				Genres.Add(g24);

				Genre g25 = new Genre();
				g25.GenreName = "Suspense";
				Genres.Add(g25);

				Genre g26 = new Genre();
				g26.GenreName = "Romance";
				Genres.Add(g26);

				Genre g27 = new Genre();
				g27.GenreName = "Mystery";
				Genres.Add(g27);

				Genre g28 = new Genre();
				g28.GenreName = "Historical Fiction";
				Genres.Add(g28);

				Genre g29 = new Genre();
				g29.GenreName = "Mystery";
				Genres.Add(g29);

				Genre g30 = new Genre();
				g30.GenreName = "Romance";
				Genres.Add(g30);

				Genre g31 = new Genre();
				g31.GenreName = "Historical Fiction";
				Genres.Add(g31);

				Genre g32 = new Genre();
				g32.GenreName = "Fantasy";
				Genres.Add(g32);

				Genre g33 = new Genre();
				g33.GenreName = "Mystery";
				Genres.Add(g33);

				Genre g34 = new Genre();
				g34.GenreName = "Mystery";
				Genres.Add(g34);

				Genre g35 = new Genre();
				g35.GenreName = "Contemporary Fiction";
				Genres.Add(g35);

				Genre g36 = new Genre();
				g36.GenreName = "Contemporary Fiction";
				Genres.Add(g36);

				Genre g37 = new Genre();
				g37.GenreName = "Thriller";
				Genres.Add(g37);

				Genre g38 = new Genre();
				g38.GenreName = "Suspense";
				Genres.Add(g38);

				Genre g39 = new Genre();
				g39.GenreName = "Mystery";
				Genres.Add(g39);

				Genre g40 = new Genre();
				g40.GenreName = "Fantasy";
				Genres.Add(g40);

				Genre g41 = new Genre();
				g41.GenreName = "Suspense";
				Genres.Add(g41);

				Genre g42 = new Genre();
				g42.GenreName = "Mystery";
				Genres.Add(g42);

				Genre g43 = new Genre();
				g43.GenreName = "Mystery";
				Genres.Add(g43);

				Genre g44 = new Genre();
				g44.GenreName = "Fantasy";
				Genres.Add(g44);

				Genre g45 = new Genre();
				g45.GenreName = "Suspense";
				Genres.Add(g45);

				Genre g46 = new Genre();
				g46.GenreName = "Historical Fiction";
				Genres.Add(g46);

				Genre g47 = new Genre();
				g47.GenreName = "Contemporary Fiction";
				Genres.Add(g47);

				Genre g48 = new Genre();
				g48.GenreName = "Humor";
				Genres.Add(g48);

				Genre g49 = new Genre();
				g49.GenreName = "Fantasy";
				Genres.Add(g49);

				Genre g50 = new Genre();
				g50.GenreName = "Thriller";
				Genres.Add(g50);

				Genre g51 = new Genre();
				g51.GenreName = "Mystery";
				Genres.Add(g51);

				Genre g52 = new Genre();
				g52.GenreName = "Historical Fiction";
				Genres.Add(g52);

				Genre g53 = new Genre();
				g53.GenreName = "Mystery";
				Genres.Add(g53);

				Genre g54 = new Genre();
				g54.GenreName = "Thriller";
				Genres.Add(g54);

				Genre g55 = new Genre();
				g55.GenreName = "Romance";
				Genres.Add(g55);

				Genre g56 = new Genre();
				g56.GenreName = "Mystery";
				Genres.Add(g56);

				Genre g57 = new Genre();
				g57.GenreName = "Contemporary Fiction";
				Genres.Add(g57);

				Genre g58 = new Genre();
				g58.GenreName = "Romance";
				Genres.Add(g58);

				Genre g59 = new Genre();
				g59.GenreName = "Mystery";
				Genres.Add(g59);

				Genre g60 = new Genre();
				g60.GenreName = "Mystery";
				Genres.Add(g60);

				Genre g61 = new Genre();
				g61.GenreName = "Thriller";
				Genres.Add(g61);

				Genre g62 = new Genre();
				g62.GenreName = "Thriller";
				Genres.Add(g62);

				Genre g63 = new Genre();
				g63.GenreName = "Romance";
				Genres.Add(g63);

				Genre g64 = new Genre();
				g64.GenreName = "Romance";
				Genres.Add(g64);

				Genre g65 = new Genre();
				g65.GenreName = "Science Fiction";
				Genres.Add(g65);

				Genre g66 = new Genre();
				g66.GenreName = "Mystery";
				Genres.Add(g66);

				Genre g67 = new Genre();
				g67.GenreName = "Historical Fiction";
				Genres.Add(g67);

				Genre g68 = new Genre();
				g68.GenreName = "Suspense";
				Genres.Add(g68);

				Genre g69 = new Genre();
				g69.GenreName = "Mystery";
				Genres.Add(g69);

				Genre g70 = new Genre();
				g70.GenreName = "Fantasy";
				Genres.Add(g70);

				Genre g71 = new Genre();
				g71.GenreName = "Fantasy";
				Genres.Add(g71);

				Genre g72 = new Genre();
				g72.GenreName = "Romance";
				Genres.Add(g72);

				Genre g73 = new Genre();
				g73.GenreName = "Romance";
				Genres.Add(g73);

				Genre g74 = new Genre();
				g74.GenreName = "Romance";
				Genres.Add(g74);

				Genre g75 = new Genre();
				g75.GenreName = "Adventure";
				Genres.Add(g75);

				Genre g76 = new Genre();
				g76.GenreName = "Contemporary Fiction";
				Genres.Add(g76);

				Genre g77 = new Genre();
				g77.GenreName = "Suspense";
				Genres.Add(g77);

				Genre g78 = new Genre();
				g78.GenreName = "Mystery";
				Genres.Add(g78);

				Genre g79 = new Genre();
				g79.GenreName = "Mystery";
				Genres.Add(g79);

				Genre g80 = new Genre();
				g80.GenreName = "Fantasy";
				Genres.Add(g80);

				Genre g81 = new Genre();
				g81.GenreName = "Romance";
				Genres.Add(g81);

				Genre g82 = new Genre();
				g82.GenreName = "Fantasy";
				Genres.Add(g82);

				Genre g83 = new Genre();
				g83.GenreName = "Suspense";
				Genres.Add(g83);

				Genre g84 = new Genre();
				g84.GenreName = "Mystery";
				Genres.Add(g84);

				Genre g85 = new Genre();
				g85.GenreName = "Mystery";
				Genres.Add(g85);

				Genre g86 = new Genre();
				g86.GenreName = "Historical Fiction";
				Genres.Add(g86);

				Genre g87 = new Genre();
				g87.GenreName = "Mystery";
				Genres.Add(g87);

				Genre g88 = new Genre();
				g88.GenreName = "Mystery";
				Genres.Add(g88);

				Genre g89 = new Genre();
				g89.GenreName = "Mystery";
				Genres.Add(g89);

				Genre g90 = new Genre();
				g90.GenreName = "Contemporary Fiction";
				Genres.Add(g90);

				Genre g91 = new Genre();
				g91.GenreName = "Romance";
				Genres.Add(g91);

				Genre g92 = new Genre();
				g92.GenreName = "Mystery";
				Genres.Add(g92);

				Genre g93 = new Genre();
				g93.GenreName = "Suspense";
				Genres.Add(g93);

				Genre g94 = new Genre();
				g94.GenreName = "Suspense";
				Genres.Add(g94);

				Genre g95 = new Genre();
				g95.GenreName = "Contemporary Fiction";
				Genres.Add(g95);

				Genre g96 = new Genre();
				g96.GenreName = "Romance";
				Genres.Add(g96);

				Genre g97 = new Genre();
				g97.GenreName = "Mystery";
				Genres.Add(g97);

				Genre g98 = new Genre();
				g98.GenreName = "Mystery";
				Genres.Add(g98);

				Genre g99 = new Genre();
				g99.GenreName = "Suspense";
				Genres.Add(g99);

				Genre g100 = new Genre();
				g100.GenreName = "Fantasy";
				Genres.Add(g100);

				Genre g101 = new Genre();
				g101.GenreName = "Mystery";
				Genres.Add(g101);

				Genre g102 = new Genre();
				g102.GenreName = "Fantasy";
				Genres.Add(g102);

				Genre g103 = new Genre();
				g103.GenreName = "Fantasy";
				Genres.Add(g103);

				Genre g104 = new Genre();
				g104.GenreName = "Science Fiction";
				Genres.Add(g104);

				Genre g105 = new Genre();
				g105.GenreName = "Science Fiction";
				Genres.Add(g105);

				Genre g106 = new Genre();
				g106.GenreName = "Mystery";
				Genres.Add(g106);

				Genre g107 = new Genre();
				g107.GenreName = "Suspense";
				Genres.Add(g107);

				Genre g108 = new Genre();
				g108.GenreName = "Romance";
				Genres.Add(g108);

				Genre g109 = new Genre();
				g109.GenreName = "Mystery";
				Genres.Add(g109);

				Genre g110 = new Genre();
				g110.GenreName = "Romance";
				Genres.Add(g110);

				Genre g111 = new Genre();
				g111.GenreName = "Suspense";
				Genres.Add(g111);

				Genre g112 = new Genre();
				g112.GenreName = "Romance";
				Genres.Add(g112);

				Genre g113 = new Genre();
				g113.GenreName = "Horror";
				Genres.Add(g113);

				Genre g114 = new Genre();
				g114.GenreName = "Romance";
				Genres.Add(g114);

				Genre g115 = new Genre();
				g115.GenreName = "Mystery";
				Genres.Add(g115);

				Genre g116 = new Genre();
				g116.GenreName = "Suspense";
				Genres.Add(g116);

				Genre g117 = new Genre();
				g117.GenreName = "Mystery";
				Genres.Add(g117);

				Genre g118 = new Genre();
				g118.GenreName = "Suspense";
				Genres.Add(g118);

				Genre g119 = new Genre();
				g119.GenreName = "Mystery";
				Genres.Add(g119);

				Genre g120 = new Genre();
				g120.GenreName = "Romance";
				Genres.Add(g120);

				Genre g121 = new Genre();
				g121.GenreName = "Mystery";
				Genres.Add(g121);

				Genre g122 = new Genre();
				g122.GenreName = "Historical Fiction";
				Genres.Add(g122);

				Genre g123 = new Genre();
				g123.GenreName = "Fantasy";
				Genres.Add(g123);

				Genre g124 = new Genre();
				g124.GenreName = "Romance";
				Genres.Add(g124);

				Genre g125 = new Genre();
				g125.GenreName = "Mystery";
				Genres.Add(g125);

				Genre g126 = new Genre();
				g126.GenreName = "Mystery";
				Genres.Add(g126);

				Genre g127 = new Genre();
				g127.GenreName = "Horror";
				Genres.Add(g127);

				Genre g128 = new Genre();
				g128.GenreName = "Mystery";
				Genres.Add(g128);

				Genre g129 = new Genre();
				g129.GenreName = "Romance";
				Genres.Add(g129);

				Genre g130 = new Genre();
				g130.GenreName = "Fantasy";
				Genres.Add(g130);

				Genre g131 = new Genre();
				g131.GenreName = "Mystery";
				Genres.Add(g131);

				Genre g132 = new Genre();
				g132.GenreName = "Mystery";
				Genres.Add(g132);

				Genre g133 = new Genre();
				g133.GenreName = "Contemporary Fiction";
				Genres.Add(g133);

				Genre g134 = new Genre();
				g134.GenreName = "Science Fiction";
				Genres.Add(g134);

				Genre g135 = new Genre();
				g135.GenreName = "Mystery";
				Genres.Add(g135);

				Genre g136 = new Genre();
				g136.GenreName = "Suspense";
				Genres.Add(g136);

				Genre g137 = new Genre();
				g137.GenreName = "Suspense";
				Genres.Add(g137);

				Genre g138 = new Genre();
				g138.GenreName = "Suspense";
				Genres.Add(g138);

				Genre g139 = new Genre();
				g139.GenreName = "Suspense";
				Genres.Add(g139);

				Genre g140 = new Genre();
				g140.GenreName = "Romance";
				Genres.Add(g140);

				Genre g141 = new Genre();
				g141.GenreName = "Mystery";
				Genres.Add(g141);

				Genre g142 = new Genre();
				g142.GenreName = "Mystery";
				Genres.Add(g142);

				Genre g143 = new Genre();
				g143.GenreName = "Thriller";
				Genres.Add(g143);

				Genre g144 = new Genre();
				g144.GenreName = "Thriller";
				Genres.Add(g144);

				Genre g145 = new Genre();
				g145.GenreName = "Mystery";
				Genres.Add(g145);

				Genre g146 = new Genre();
				g146.GenreName = "Fantasy";
				Genres.Add(g146);

				Genre g147 = new Genre();
				g147.GenreName = "Humor";
				Genres.Add(g147);

				Genre g148 = new Genre();
				g148.GenreName = "Fantasy";
				Genres.Add(g148);

				Genre g149 = new Genre();
				g149.GenreName = "Suspense";
				Genres.Add(g149);

				Genre g150 = new Genre();
				g150.GenreName = "Historical Fiction";
				Genres.Add(g150);

				Genre g151 = new Genre();
				g151.GenreName = "Mystery";
				Genres.Add(g151);

				Genre g152 = new Genre();
				g152.GenreName = "Fantasy";
				Genres.Add(g152);

				Genre g153 = new Genre();
				g153.GenreName = "Thriller";
				Genres.Add(g153);

				Genre g154 = new Genre();
				g154.GenreName = "Historical Fiction";
				Genres.Add(g154);

				Genre g155 = new Genre();
				g155.GenreName = "Suspense";
				Genres.Add(g155);

				Genre g156 = new Genre();
				g156.GenreName = "Contemporary Fiction";
				Genres.Add(g156);

				Genre g157 = new Genre();
				g157.GenreName = "Contemporary Fiction";
				Genres.Add(g157);

				Genre g158 = new Genre();
				g158.GenreName = "Suspense";
				Genres.Add(g158);

				Genre g159 = new Genre();
				g159.GenreName = "Mystery";
				Genres.Add(g159);

				Genre g160 = new Genre();
				g160.GenreName = "Thriller";
				Genres.Add(g160);

				Genre g161 = new Genre();
				g161.GenreName = "Suspense";
				Genres.Add(g161);

				Genre g162 = new Genre();
				g162.GenreName = "Historical Fiction";
				Genres.Add(g162);

				Genre g163 = new Genre();
				g163.GenreName = "Mystery";
				Genres.Add(g163);

				Genre g164 = new Genre();
				g164.GenreName = "Mystery";
				Genres.Add(g164);

				Genre g165 = new Genre();
				g165.GenreName = "Suspense";
				Genres.Add(g165);

				Genre g166 = new Genre();
				g166.GenreName = "Mystery";
				Genres.Add(g166);

				Genre g167 = new Genre();
				g167.GenreName = "Romance";
				Genres.Add(g167);

				Genre g168 = new Genre();
				g168.GenreName = "Romance";
				Genres.Add(g168);

				Genre g169 = new Genre();
				g169.GenreName = "Fantasy";
				Genres.Add(g169);

				Genre g170 = new Genre();
				g170.GenreName = "Poetry";
				Genres.Add(g170);

				Genre g171 = new Genre();
				g171.GenreName = "Suspense";
				Genres.Add(g171);

				Genre g172 = new Genre();
				g172.GenreName = "Mystery";
				Genres.Add(g172);

				Genre g173 = new Genre();
				g173.GenreName = "Romance";
				Genres.Add(g173);

				Genre g174 = new Genre();
				g174.GenreName = "Horror";
				Genres.Add(g174);

				Genre g175 = new Genre();
				g175.GenreName = "Historical Fiction";
				Genres.Add(g175);

				Genre g176 = new Genre();
				g176.GenreName = "Romance";
				Genres.Add(g176);

				Genre g177 = new Genre();
				g177.GenreName = "Thriller";
				Genres.Add(g177);

				Genre g178 = new Genre();
				g178.GenreName = "Science Fiction";
				Genres.Add(g178);

				Genre g179 = new Genre();
				g179.GenreName = "Historical Fiction";
				Genres.Add(g179);

				Genre g180 = new Genre();
				g180.GenreName = "Mystery";
				Genres.Add(g180);

				Genre g181 = new Genre();
				g181.GenreName = "Suspense";
				Genres.Add(g181);

				Genre g182 = new Genre();
				g182.GenreName = "Suspense";
				Genres.Add(g182);

				Genre g183 = new Genre();
				g183.GenreName = "Mystery";
				Genres.Add(g183);

				Genre g184 = new Genre();
				g184.GenreName = "Historical Fiction";
				Genres.Add(g184);

				Genre g185 = new Genre();
				g185.GenreName = "Science Fiction";
				Genres.Add(g185);

				Genre g186 = new Genre();
				g186.GenreName = "Contemporary Fiction";
				Genres.Add(g186);

				Genre g187 = new Genre();
				g187.GenreName = "Contemporary Fiction";
				Genres.Add(g187);

				Genre g188 = new Genre();
				g188.GenreName = "Contemporary Fiction";
				Genres.Add(g188);

				Genre g189 = new Genre();
				g189.GenreName = "Mystery";
				Genres.Add(g189);

				Genre g190 = new Genre();
				g190.GenreName = "Fantasy";
				Genres.Add(g190);

				Genre g191 = new Genre();
				g191.GenreName = "Fantasy";
				Genres.Add(g191);

				Genre g192 = new Genre();
				g192.GenreName = "Fantasy";
				Genres.Add(g192);

				Genre g193 = new Genre();
				g193.GenreName = "Contemporary Fiction";
				Genres.Add(g193);

				Genre g194 = new Genre();
				g194.GenreName = "Fantasy";
				Genres.Add(g194);

				Genre g195 = new Genre();
				g195.GenreName = "Fantasy";
				Genres.Add(g195);

				Genre g196 = new Genre();
				g196.GenreName = "Suspense";
				Genres.Add(g196);

				Genre g197 = new Genre();
				g197.GenreName = "Historical Fiction";
				Genres.Add(g197);

				Genre g198 = new Genre();
				g198.GenreName = "Mystery";
				Genres.Add(g198);

				Genre g199 = new Genre();
				g199.GenreName = "Historical Fiction";
				Genres.Add(g199);

				Genre g200 = new Genre();
				g200.GenreName = "Contemporary Fiction";
				Genres.Add(g200);

				Genre g201 = new Genre();
				g201.GenreName = "Historical Fiction";
				Genres.Add(g201);

				Genre g202 = new Genre();
				g202.GenreName = "Mystery";
				Genres.Add(g202);

				Genre g203 = new Genre();
				g203.GenreName = "Mystery";
				Genres.Add(g203);

				Genre g204 = new Genre();
				g204.GenreName = "Mystery";
				Genres.Add(g204);

				Genre g205 = new Genre();
				g205.GenreName = "Mystery";
				Genres.Add(g205);

				Genre g206 = new Genre();
				g206.GenreName = "Romance";
				Genres.Add(g206);

				Genre g207 = new Genre();
				g207.GenreName = "Suspense";
				Genres.Add(g207);

				Genre g208 = new Genre();
				g208.GenreName = "Suspense";
				Genres.Add(g208);

				Genre g209 = new Genre();
				g209.GenreName = "Mystery";
				Genres.Add(g209);

				Genre g210 = new Genre();
				g210.GenreName = "Poetry";
				Genres.Add(g210);

				Genre g211 = new Genre();
				g211.GenreName = "Thriller";
				Genres.Add(g211);

				Genre g212 = new Genre();
				g212.GenreName = "Contemporary Fiction";
				Genres.Add(g212);

				Genre g213 = new Genre();
				g213.GenreName = "Humor";
				Genres.Add(g213);

				Genre g214 = new Genre();
				g214.GenreName = "Romance";
				Genres.Add(g214);

				Genre g215 = new Genre();
				g215.GenreName = "Romance";
				Genres.Add(g215);

				Genre g216 = new Genre();
				g216.GenreName = "Suspense";
				Genres.Add(g216);

				Genre g217 = new Genre();
				g217.GenreName = "Mystery";
				Genres.Add(g217);

				Genre g218 = new Genre();
				g218.GenreName = "Historical Fiction";
				Genres.Add(g218);

				Genre g219 = new Genre();
				g219.GenreName = "Historical Fiction";
				Genres.Add(g219);

				Genre g220 = new Genre();
				g220.GenreName = "Romance";
				Genres.Add(g220);

				Genre g221 = new Genre();
				g221.GenreName = "Mystery";
				Genres.Add(g221);

				Genre g222 = new Genre();
				g222.GenreName = "Thriller";
				Genres.Add(g222);

				Genre g223 = new Genre();
				g223.GenreName = "Science Fiction";
				Genres.Add(g223);

				Genre g224 = new Genre();
				g224.GenreName = "Mystery";
				Genres.Add(g224);

				Genre g225 = new Genre();
				g225.GenreName = "Historical Fiction";
				Genres.Add(g225);

				Genre g226 = new Genre();
				g226.GenreName = "Historical Fiction";
				Genres.Add(g226);

				Genre g227 = new Genre();
				g227.GenreName = "Romance";
				Genres.Add(g227);

				Genre g228 = new Genre();
				g228.GenreName = "Mystery";
				Genres.Add(g228);

				Genre g229 = new Genre();
				g229.GenreName = "Thriller";
				Genres.Add(g229);

				Genre g230 = new Genre();
				g230.GenreName = "Mystery";
				Genres.Add(g230);

				Genre g231 = new Genre();
				g231.GenreName = "Romance";
				Genres.Add(g231);

				Genre g232 = new Genre();
				g232.GenreName = "Suspense";
				Genres.Add(g232);

				Genre g233 = new Genre();
				g233.GenreName = "Mystery";
				Genres.Add(g233);

				Genre g234 = new Genre();
				g234.GenreName = "Thriller";
				Genres.Add(g234);

				Genre g235 = new Genre();
				g235.GenreName = "Suspense";
				Genres.Add(g235);

				Genre g236 = new Genre();
				g236.GenreName = "Mystery";
				Genres.Add(g236);

				Genre g237 = new Genre();
				g237.GenreName = "Science Fiction";
				Genres.Add(g237);

				Genre g238 = new Genre();
				g238.GenreName = "Suspense";
				Genres.Add(g238);

				Genre g239 = new Genre();
				g239.GenreName = "Mystery";
				Genres.Add(g239);

				Genre g240 = new Genre();
				g240.GenreName = "Suspense";
				Genres.Add(g240);

				Genre g241 = new Genre();
				g241.GenreName = "Contemporary Fiction";
				Genres.Add(g241);

				Genre g242 = new Genre();
				g242.GenreName = "Contemporary Fiction";
				Genres.Add(g242);

				Genre g243 = new Genre();
				g243.GenreName = "Mystery";
				Genres.Add(g243);

				Genre g244 = new Genre();
				g244.GenreName = "Mystery";
				Genres.Add(g244);

				Genre g245 = new Genre();
				g245.GenreName = "Thriller";
				Genres.Add(g245);

				Genre g246 = new Genre();
				g246.GenreName = "Contemporary Fiction";
				Genres.Add(g246);

				Genre g247 = new Genre();
				g247.GenreName = "Historical Fiction";
				Genres.Add(g247);

				Genre g248 = new Genre();
				g248.GenreName = "Fantasy";
				Genres.Add(g248);

				Genre g249 = new Genre();
				g249.GenreName = "Contemporary Fiction";
				Genres.Add(g249);

				Genre g250 = new Genre();
				g250.GenreName = "Mystery";
				Genres.Add(g250);

				Genre g251 = new Genre();
				g251.GenreName = "Suspense";
				Genres.Add(g251);

				Genre g252 = new Genre();
				g252.GenreName = "Romance";
				Genres.Add(g252);

				Genre g253 = new Genre();
				g253.GenreName = "Contemporary Fiction";
				Genres.Add(g253);

				Genre g254 = new Genre();
				g254.GenreName = "Suspense";
				Genres.Add(g254);

				Genre g255 = new Genre();
				g255.GenreName = "Fantasy";
				Genres.Add(g255);

				Genre g256 = new Genre();
				g256.GenreName = "Suspense";
				Genres.Add(g256);

				Genre g257 = new Genre();
				g257.GenreName = "Mystery";
				Genres.Add(g257);

				Genre g258 = new Genre();
				g258.GenreName = "Suspense";
				Genres.Add(g258);

				Genre g259 = new Genre();
				g259.GenreName = "Historical Fiction";
				Genres.Add(g259);

				Genre g260 = new Genre();
				g260.GenreName = "Science Fiction";
				Genres.Add(g260);

				Genre g261 = new Genre();
				g261.GenreName = "Romance";
				Genres.Add(g261);

				Genre g262 = new Genre();
				g262.GenreName = "Suspense";
				Genres.Add(g262);

				Genre g263 = new Genre();
				g263.GenreName = "Fantasy";
				Genres.Add(g263);

				Genre g264 = new Genre();
				g264.GenreName = "Historical Fiction";
				Genres.Add(g264);

				Genre g265 = new Genre();
				g265.GenreName = "Suspense";
				Genres.Add(g265);

				Genre g266 = new Genre();
				g266.GenreName = "Mystery";
				Genres.Add(g266);

				Genre g267 = new Genre();
				g267.GenreName = "Fantasy";
				Genres.Add(g267);

				Genre g268 = new Genre();
				g268.GenreName = "Suspense";
				Genres.Add(g268);

				Genre g269 = new Genre();
				g269.GenreName = "Mystery";
				Genres.Add(g269);

				Genre g270 = new Genre();
				g270.GenreName = "Mystery";
				Genres.Add(g270);

				Genre g271 = new Genre();
				g271.GenreName = "Thriller";
				Genres.Add(g271);

				Genre g272 = new Genre();
				g272.GenreName = "Contemporary Fiction";
				Genres.Add(g272);

				Genre g273 = new Genre();
				g273.GenreName = "Suspense";
				Genres.Add(g273);

				Genre g274 = new Genre();
				g274.GenreName = "Mystery";
				Genres.Add(g274);

				Genre g275 = new Genre();
				g275.GenreName = "Mystery";
				Genres.Add(g275);

				Genre g276 = new Genre();
				g276.GenreName = "Mystery";
				Genres.Add(g276);

				Genre g277 = new Genre();
				g277.GenreName = "Thriller";
				Genres.Add(g277);

				Genre g278 = new Genre();
				g278.GenreName = "Mystery";
				Genres.Add(g278);

				Genre g279 = new Genre();
				g279.GenreName = "Mystery";
				Genres.Add(g279);

				Genre g280 = new Genre();
				g280.GenreName = "Suspense";
				Genres.Add(g280);

				Genre g281 = new Genre();
				g281.GenreName = "Contemporary Fiction";
				Genres.Add(g281);

				Genre g282 = new Genre();
				g282.GenreName = "Mystery";
				Genres.Add(g282);

				Genre g283 = new Genre();
				g283.GenreName = "Mystery";
				Genres.Add(g283);

				Genre g284 = new Genre();
				g284.GenreName = "Science Fiction";
				Genres.Add(g284);

				Genre g285 = new Genre();
				g285.GenreName = "Thriller";
				Genres.Add(g285);

				Genre g286 = new Genre();
				g286.GenreName = "Mystery";
				Genres.Add(g286);

				Genre g287 = new Genre();
				g287.GenreName = "Fantasy";
				Genres.Add(g287);

				Genre g288 = new Genre();
				g288.GenreName = "Fantasy";
				Genres.Add(g288);

				Genre g289 = new Genre();
				g289.GenreName = "Mystery";
				Genres.Add(g289);

				Genre g290 = new Genre();
				g290.GenreName = "Contemporary Fiction";
				Genres.Add(g290);

				Genre g291 = new Genre();
				g291.GenreName = "Mystery";
				Genres.Add(g291);

				Genre g292 = new Genre();
				g292.GenreName = "Science Fiction";
				Genres.Add(g292);

				Genre g293 = new Genre();
				g293.GenreName = "Mystery";
				Genres.Add(g293);

				Genre g294 = new Genre();
				g294.GenreName = "Suspense";
				Genres.Add(g294);

				Genre g295 = new Genre();
				g295.GenreName = "Shakespeare";
				Genres.Add(g295);

				Genre g296 = new Genre();
				g296.GenreName = "Mystery";
				Genres.Add(g296);

				Genre g297 = new Genre();
				g297.GenreName = "Historical Fiction";
				Genres.Add(g297);

				Genre g298 = new Genre();
				g298.GenreName = "Romance";
				Genres.Add(g298);

				Genre g299 = new Genre();
				g299.GenreName = "Contemporary Fiction";
				Genres.Add(g299);

				Genre g300 = new Genre();
				g300.GenreName = "Mystery";
				Genres.Add(g300);

				//loop through genres
				foreach (Genre genre in Genres)
				{
					
					//see if genre exists in database
					Genre dbGenre = db.Genres.FirstOrDefault(r => r.GenreName == genre.GenreName);

					if (dbGenre == null) //genre does not exist in database
					{
						db.Genres.Add(genre);
						db.SaveChanges();
						intGenresAdded += 1;
					}
					
				}
			}
			catch
			{
                String msg = "Genres added:" + intGenresAdded.ToString();
				throw new InvalidOperationException(msg);
			}
		}
	}
}
