using System;
using ListMyBikes.Data;
using ListMyBikes.Models;

namespace ListMyBikes.DAL
{
    public class UnitOfWork : IDisposable
    {
        private BikeContext bikeContext = new BikeContext();
        private GenericRespository<Bike> bikeRepository;

        public UnitOfWork(){}

        public GenericRespository<Bike> BikeRepository
        {
            get
            {
                if(this.bikeRepository == null){
                    this.bikeRepository = new GenericRespository<Bike>(bikeContext);
                }
                return this.bikeRepository;
            }
        }

        public void Save() => bikeContext.SaveChanges();

        private bool disposed = false;

        protected virtual void Dispose(bool disposing){
            if(!this.disposed){
                if(disposing){
                    bikeContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}