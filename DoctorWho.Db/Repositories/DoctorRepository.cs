﻿using DoctorWho.Db.Entities;
using DoctorWho.Db.Services;
using Microsoft.EntityFrameworkCore;

namespace DoctorWho.Db.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly DoctorWhoCoreDbContext _context;

    public DoctorRepository(DoctorWhoCoreDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public void AddDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public void ModifyDoctorName(Doctor doctor, string newName)
    {
        doctor.DoctorName = newName;
        _context.SaveChanges();
    }
    
    public void ModifyDoctorNumber(Doctor doctor, int newNumber)
    {
        doctor.DoctorNumber = newNumber;
        _context.SaveChanges();
    }
    
    public void ModifyDoctorBirth(Doctor doctor, DateTime newBirth)
    {
        doctor.BirthDate = newBirth;
        _context.SaveChanges();
    }
    
    public void ModifyDoctorFirstEpisode(Doctor doctor, DateTime newDate)
    {
        doctor.FirstEpisodeDate = newDate;
        _context.SaveChanges();
    }
    public void ModifyDoctorLastEpisode(Doctor doctor, DateTime newDate)
    {
        doctor.LastEpisodeDate = newDate;
        _context.SaveChanges();
    }

    public void DeleteDoctor(int doctorId)
    {
        var doctorToDelete = _context.Doctors.FirstOrDefault(d => d.DoctorId.Equals(doctorId));

        if (doctorToDelete == default)
            throw new Exception($"Doctor with id {doctorId} not found!");
        
        _context.Doctors.Remove(doctorToDelete);
        _context.SaveChanges();
    }

    public ICollection<Doctor> GetAll()
    {
        return _context.Doctors.ToList();
    }

    public async Task<(IEnumerable<Doctor>, PaginationMetadata)> GetDoctorsAsync(int pageNumber, int pageSize)
    {
        var collection = _context.Doctors as IQueryable<Doctor>;

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var collectionToReturn = await collection.OrderBy(d => d.DoctorName)
            .Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

        return (collectionToReturn, paginationMetadata);

    }
}