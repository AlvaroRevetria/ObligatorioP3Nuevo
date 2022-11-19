using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excepciones;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioPartidos : IRepositorioPartidos
    {
        public LibreriaContext Contexto { get; set; }

        public RepositorioPartidos(LibreriaContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(Partido nuevo)
        {
            try
            {

                nuevo.Validar();
               
                Horario horarioPartido = Contexto.Horarios.Where(h => h.Hora == nuevo.Hora.Hora).SingleOrDefault();
                if (horarioPartido == null)
                {
                    throw new PartidoException("El horario indicado no es un horario válido");
                }
                nuevo.Hora = horarioPartido;

                Seleccion seleccion1 = Contexto.Selecciones.Include(s => s.Grupo).Where(s => s.Id == nuevo.Seleccion1.Id).SingleOrDefault();
                Seleccion seleccion2 = Contexto.Selecciones.Include(s => s.Grupo).Where(s => s.Id == nuevo.Seleccion2.Id).SingleOrDefault();

                if( seleccion1 == null || seleccion2 == null)
                {
                    throw new PartidoException("Ambas selecciones deben existir para poder cargar un partido entre ellas");
                }

                if (seleccion1.Grupo.Nombre != seleccion2.Grupo.Nombre)
                {
                    throw new PartidoException("Las selecciones deben pertenecer al mismo grupo");
                }

                IEnumerable<SeleccionPartido> partidosSeleccion1 = Contexto.SeleccionPartido
                    .Where(ps1 => ps1.SeleccionId == nuevo.Seleccion1.Id)
                    .ToList();

                IEnumerable<SeleccionPartido> partidosSeleccion2 = Contexto.SeleccionPartido
                    .Where(ps1 => ps1.SeleccionId == nuevo.Seleccion2.Id)
                    .ToList();

                if (partidosSeleccion1.Count() >= 3 || partidosSeleccion2.Count() >= 3)
                {
                    throw new PartidoException("El máximo de partidos por seleción es 3");
                }

                foreach (var p1 in partidosSeleccion1)
                {
                    foreach (var p2 in partidosSeleccion2)
                    {
                        if(p1.PartidoId == p2.PartidoId)
                        {
                            throw new PartidoException("Estas selecciones ya jugaron entre sí");
                        }
                    }
                }

                IEnumerable<Partido> partidos = FindAll();

                foreach (var partido in partidos)
                {
                    if(partido.Fecha == nuevo.Fecha)
                    {
                        if(partido.Hora.Hora == nuevo.Hora.Hora)
                        {
                            throw new PartidoException("Ya existe un partido en este horario");
                        }
                    }
                }
                

                Contexto.Partidos.Add(nuevo);
                Contexto.SaveChanges();
            }
            catch (PartidoException)
            {
                throw;
            }
            catch (HoraException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo dar de alta el partido", e);
            }
        }

        public IEnumerable<Partido> FindAll()
        {
            try
            {
                return Contexto.Partidos
                    .Include(p => p.Hora)
                    .Include(p => p.Seleccion1)
                    .Include(p => p.Seleccion2)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudieron encontrar partidos", e);
            }
        }

        public Partido FindById(int Id)
        {
            try
            {
                if (Id == 0) throw new Exception("El id de partido no puede ser 0");
                return Contexto.Partidos
                    .Include(p => p.Hora)
                    .Include(p => p.Seleccion1)
                    .Include(p => p.Seleccion2)
                    .Where(p => p.Id == Id)
                    .SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo encontrar el partido", e);
            }
        }

        public void Remove(int Id)
        {
            try
            {
                //Validar que no se encuentre vinculado con ninguna seleccion o grupo. 
                Partido partidoABorrar = FindById(Id);
                if (partidoABorrar == null) throw new Exception("No existe el partido a borrar");
                Contexto.Partidos.Remove(partidoABorrar);
                Contexto.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo borrar el partido", e);
            }
        }

        public void Update(Partido partido)
        {
            try
            {
                partido.Validar();
                Seleccion seleccion1 = Contexto.Selecciones.Find(partido.Seleccion1.Id);
                Seleccion seleccion2 = Contexto.Selecciones.Find(partido.Seleccion2.Id);
                Horario horarioPartido = Contexto.Horarios.Where(h => h.Hora == partido.Hora.Hora).SingleOrDefault();
                if (horarioPartido == null)
                {
                    throw new PartidoException("El horario indicado no es un horario válido");
                }
                ActualizarPuntosSelecciones(partido, seleccion1, seleccion2);
                partido.Seleccion1 = seleccion1;
                partido.Seleccion2 = seleccion2;
                partido.Hora = horarioPartido;
                partido.Estado = "FINALIZADO";
                Contexto.Partidos.Update(partido);
                Contexto.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo actualizar el partido", e);
            }
        }

        private void ActualizarPuntosSelecciones(Partido partido, Seleccion seleccion1, Seleccion seleccion2)
        {

            if (partido.GolesS1 > partido.GolesS2)
            {
                int puntosSeleccion1 = 3;
                seleccion1.Puntos += puntosSeleccion1;

                Contexto.Selecciones.Update(seleccion1);
            }
            else if (partido.GolesS1 < partido.GolesS2)
            {
                int puntosSeleccion2 = 3;
                seleccion2.Puntos += puntosSeleccion2;

                Contexto.Selecciones.Update(seleccion2);
            }
            else
            {
                int puntosSeleccion1 = 1;
                int puntosSeleccion2 = 1;
                seleccion1.Puntos += puntosSeleccion1;
                seleccion2.Puntos += puntosSeleccion2;

                Contexto.Selecciones.Update(seleccion1);
                Contexto.Selecciones.Update(seleccion2);
            }
        }

    }
}
