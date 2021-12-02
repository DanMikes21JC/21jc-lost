create view GroupeBilletSacVoitureSemaine as
	select
    g.nom,
    s.numero,
    sum(coalesce(tbillet.qty, 0)) "billet",
    sum(coalesce(tsac.qty, 0)) "sac",
    sum(coalesce(tvoiture.qty, 0)) "voiture",
    sum(coalesce(tbillet.qty, 0) * (tb.valeur_billet / 100)) + sum(coalesce(tsac.qty, 0) * (150 - tb.valeur_sac)) + sum(coalesce(tvoiture.qty, 0) * (2000 - tb.valeur_voiture)) "benefice"
    from "Groupe" g
    left outer join "Semaine" s on 1=1
    left outer join "TauxBlanchiment" tb on tb.id_groupe = g.id 
    left outer join "Transaction" tbillet on tbillet.id_semaine = s.id and tbillet."Discriminator" = 'Billet' and tbillet.id_taux_blanchiment = tb.id
    left outer join "Transaction" tsac on tsac.id_semaine = s.id and tsac."Discriminator" = 'Sac' and tsac.id_taux_blanchiment = tb.id
    left outer join "Transaction" tvoiture on tvoiture.id_semaine = s.id and tvoiture."Discriminator" = 'Voiture' and tvoiture.id_taux_blanchiment = tb.id
    where tbillet.qty is not null
    or tsac.qty is not null
    or tvoiture.qty is not null
    group by s.numero, g.nom
    order by s.numero, g.nom;
    
create view PersonneBilletSacVoitureSemaine as
	select
    p.nom "nom",
    s.numero,
    sum(coalesce(tbillet.qty, 0)) "billet",
    sum(coalesce(tsac.qty, 0)) "sac",
    sum(coalesce(tvoiture.qty, 0)) "voiture",
    sum(coalesce(tbillet.qty, 0) * (tb.valeur_billet / 100)) + sum(coalesce(tsac.qty, 0) * (150 - tb.valeur_sac)) + sum(coalesce(tvoiture.qty, 0) * (2000 - tb.valeur_voiture)) "benefice"
    from "Personne" p
    left outer join "Semaine" s on 1=1
    left outer join "TauxBlanchiment" tb on tb.id_personne = p.id 
    left outer join "Transaction" tbillet on tbillet.id_semaine = s.id and tbillet."Discriminator" = 'Billet' and tbillet.id_taux_blanchiment = tb.id
    left outer join "Transaction" tsac on tsac.id_semaine = s.id and tsac."Discriminator" = 'Sac' and tsac.id_taux_blanchiment = tb.id
    left outer join "Transaction" tvoiture on tvoiture.id_semaine = s.id and tvoiture."Discriminator" = 'Voiture' and tvoiture.id_taux_blanchiment = tb.id
    where tbillet.qty is not null
    or tsac.qty is not null
    or tvoiture.qty is not null
    group by s.numero, p.nom
    order by s.numero, p.nom;