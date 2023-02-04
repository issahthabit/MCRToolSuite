SELECT InformantDistrict, InformantTA,InformantVillage,COUNT(*) FROM ChildDetail WHERE brn<>'' AND ben<>'' GROUP BY InformantDistrict, InformantTA,InformantVillage 
HAVING COUNT(*)=50
ORDER BY COUNT(*) DESC


SELECT InformantDistrict, InformantTA,InformantVillage,COUNT(*) FROM ChildDetail WHERE InformantDistrict NOT IN ('LILONGWE','KARONGA','MWANZA')