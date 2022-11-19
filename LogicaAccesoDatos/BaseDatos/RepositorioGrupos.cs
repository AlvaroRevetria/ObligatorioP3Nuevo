using System;
using System.Collections.Generic;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Dominio;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Excepciones;

namespace LogicaAccesoDatos.BaseDatos
{
    public class RepositorioGrupos : IRepositorioGrupos
    {
        public LibreriaContext Contexto { get; set; }

       public RepositorioGrupos(LibreriaContext ctx)
        {
            Contexto = ctx;
        }   
        
        public void Add(Grupo nuevo)
        {
            try
            {
                nuevo.Validar();
                Contexto.Grupos.Add(nuevo);
                Contexto.SaveChanges();
            }
            catch (GrupoException)
            {
                throw;
            }
            catch(Exception e)
            {
                throw new Exception("No se puedo dar de alta el grupo", e);
            }
        }

        public IEnumerable<Grupo> FindAll()
        {
            try
            {
                return Contexto.Grupos.ToList();
            } catch (Exception e)
            {
                throw new Exception("No se pudo encontrar grupo", e);
            }
        }

        public Grupo FindById(int Id)
        {
            try
            {
                if (Id == 0) throw new GrupoException("El id de grupo no puede ser 0");
                return Contexto.Grupos.Find(Id);
            }
            catch (GrupoException)
            {
                throw;
            }

            catch (Exception e)
            {
                throw new Exception("No se pudo encontrar el grupo", e);
            }
        }

        public void Remove(int Id)
        {
            try
            {            
                //Validar que no se encuentre vinculado con ninguna seleccion o partido. 
                Grupo grupoABorrar = FindById(Id);
                if (grupoABorrar == null) throw new GrupoException("No existe el grupo a borrar");

                Contexto.Grupos.Remove(grupoABorrar);
                Contexto.SaveChanges();
            }
            catch (GrupoException)
            {
                throw;
            }

            catch (Exception e)
            {
                throw new Exception("No se pudo encontrar el grupo", e);
            }

        }

        public void Update(Grupo nuevo)
        {
            try
            {
                nuevo.Validar();
                Contexto.Grupos.Update(nuevo);
                Contexto.SaveChanges();
            } catch (Exception e)
            {
                throw new Exception("No se pudo encontrar el grupo", e);
            }
        }

        public IEnumerable<Grupo> DatosPartidos(string nomGrupo)
        {

            try
            {
                return Contexto.Grupos
                           .Where(g => g.Nombre == nomGrupo)
                           .Include(g => g.PartidoGrupo)
                           .ThenInclude(pg => pg.Partido)
                           .Include(g => g.PartidoGrupo)
                           .ThenInclude(pg => pg.Partido)
                           .Include(g => g.PartidoGrupo)
                           .ThenInclude(pg => pg.Partido)
                           .ThenInclude(p => p.Hora)
                           .Include(g => g.PartidoGrupo)
                           .ThenInclude(pg => pg.Partido)
                           .ThenInclude(p => p.Seleccion1)
                           .ThenInclude(s => s.Pais)
                           .Include(g => g.PartidoGrupo)
                           .ThenInclude(pg => pg.Partido)
                           .ThenInclude(p => p.Seleccion2)
                           .ThenInclude(s => s.Pais)
                           .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo encontrar el grupo", e);
            }

       }
    }
}
