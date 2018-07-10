var express = require('express');
var router = express.Router();
var promise = require('bluebird');
var options = {promiseLib: promise};
var pgp = require('pg-promise')(options);
var connectionString = 'postgresql://postgres:@lumno123@localhost:5432/Torneo';
var db = pgp(connectionString);

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Express' });
});

router.get('/api/fixture', obtenerDatos);
router.get('/api/equipos', obtenerEquipo);
router.get('/api/fixture/:id', obtenerUnDato);
router.post('/api/fixture', crearDato);
router.put('/api/fixture/:id', actualizarDato);
router.delete('/api/fixture/:id', eliminarDato);

function obtenerDatos(req,res,next){
  db.any('select p.id_partido, t.nombre as Torneo, el.nombre as EquipoLocal, ev.nombre as EquipoVisitante, p.fecha_nro as Fecha from partido p '+ 
  'inner join torneo t on p.torneo_id = t.id_torneo '+ 
  'inner join equipo el on p.equipo_local_id = el.id_equipo '+
  'inner join equipo ev on p.equipo_visitante_id = ev.id_equipo '+ 
  'order by p.id_partido, t.nombre , el.nombre, ev.nombre, p.fecha_nro')
  //db.any('select * from partido')
  .then(function (data){
    res.status(200).json({
      status: 'correcto!',
      data : data,
      message: 'Consulta realizada correctamente'
    });
  })
  .catch(function(err){
    return next(err);
  });
}

function obtenerUnDato(req, res, next) {
  var datoID = parseInt(req.params.id);
  //db.one("select * from partido where id_partido = $1", datoID)
  db.one('select * from partido where equipo_local_id = $1 or equipo_visitante_id = $1', datoID)  
	.then(function (data) {
      res.status(200)
        .json({
          status: 'correcto!',
          data: data,
          message: 'Obtener un dato'
        });
    })
    .catch(function (err) {
      return next(err);
    });
}

function obtenerEquipo(req,res,next){
	db.any('select nombre from equipo')
	.then(function(data){
		res.status(200).json({
			status : 'correcto!',
			data : data,
			message: 'Consulta realizada correctamente'
		});
	})
	.catch(function(err){
		return next(err);
	});
}

function crearDato(req, res, next) {
  //req.body.torneo_id = parseInt(req.body.id);
  //req.body.equipo_id = parseInt(req.body.id);
  db.result("insert into torneo_equipos(torneo_id, equipo_id)" +
      " values(${torneo_id}, ${equipo_id})",
    req.body)
    .then(function (result) {
      res.status(200)
        .json({
          status: 'success',
          message: 'Un equipo registrado a un torneo'
        });
    })
    .catch(function (err) {
      return next(err);
    });
}

function actualizarDato(req, res, next) {
  db.none('update torneo_equipos set torneo_id=$1, equipo_id=$2',
    [req.body.torneo_id, req.body.equipo_id])
    .then(function () {
      res.status(200)
        .json({
          status: 'success',
          message: 'Equipo modificado'
        });
    })
    .catch(function (err) {
      return next(err);
    });
}

function eliminarDato(req, res, next) {
  var datoID = parseInt(req.params.id);
  //var datoID = req.params.equipo_id;
  db.result('delete from torneo_equipos where equipo_id = $1', datoID)
    .then(function (result) {
      /* jshint ignore:start*/ 
      res.status(200)
        .json({
          status: 'success',
          message: `Equipo ${result.rowCount} borrado`
        });
      /* jshint ignore:end */
    })
    .catch(function (err) {
      return next(err);
    });
}

module.exports = router;
