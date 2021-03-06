SELECT 
 ci.codigo_inciso, ci.texto_inciso, ci.dai_base, 
 sc.estado, 
 sc.inciso_nuevo as codigo_inciso_corr, 
 sc.texto_inciso as texto_inciso_corr, sc.dai_nuevo as dai_corr 
 FROM 
 SAC_Incisos CI 
LEFT OUTER JOIN
(SELECT 
inciso_origen, inciso_nuevo, 
texto_inciso, dai_nuevo, 
anio_version, version,
CASE WHEN (inciso_nuevo IS NULL)  THEN 'SUPRIMIDA' ELSE 'APERTURA' END as estado
FROM
SAC_Correlacion) SC ON
sc.inciso_origen = ci.codigo_inciso AND
sc.version = ci.id_version AND
sc.anio_version = ci.anio_version 
WHERE 
 CI.estado = 'A' AND  
 CI.codigo_inciso LIKE '01%' 