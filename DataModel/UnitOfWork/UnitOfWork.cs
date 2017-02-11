#region Using Namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity.Validation;
using DataModel.GenericRepository;

#endregion

namespace DataModel.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        #region Private member variables...

        private FitnessClubDbEntities _context = null;
        private GenericRepository<User> _userRepository;
        private GenericRepository<Token> _tokenRepository;
        private GenericRepository<Registration> _registrationRepository;
        private GenericRepository<Training> _trainingRepository;
        private GenericRepository<Goal> _goalRepository;
        private GenericRepository<RegTraining> _RegTrainingRepository;
        private GenericRepository<RegGoal> _regGoalRepository;
        private GenericRepository<Status> _statusRepository;
        private GenericRepository<TimeSlot> _timeSlotRepository;
        private GenericRepository<ImageDetail> _imageDetailRepository;
        private GenericRepository<AdMedia> _adMediaRepository;
        
        #endregion

        public UnitOfWork()
        {
            _context = new FitnessClubDbEntities();
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<User>(_context);
                return _userRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        public GenericRepository<Token> TokenRepository
        {
            get
            {
                if (this._tokenRepository == null)
                    this._tokenRepository = new GenericRepository<Token>(_context);
                return _tokenRepository;
            }
        }
        #endregion

        /// <summary>
        /// Get/Set Property for registration repository.
        /// </summary>
        public GenericRepository<Registration> RegistrationRepository
        {
            get
            {
                if (this._registrationRepository == null)
                    this._registrationRepository = new GenericRepository<Registration>(_context);
                return _registrationRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for training repository.
        /// </summary>
        public GenericRepository<Training> TrainingRepository
        {
            get
            {
                if (this._trainingRepository == null)
                    this._trainingRepository = new GenericRepository<Training>(_context);
                return _trainingRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for goal repository.
        /// </summary>
        public GenericRepository<Goal> GoalRepository
        {
            get
            {
                if (this._goalRepository == null)
                    this._goalRepository = new GenericRepository<Goal>(_context);
                return _goalRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for User repository.
        /// </summary>
        public GenericRepository<RegTraining> RegTrainingRepository
        {
            get
            {
                if (this._RegTrainingRepository == null)
                    this._RegTrainingRepository = new GenericRepository<RegTraining>(_context);
                return _RegTrainingRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for registration repository.
        /// </summary>
        public GenericRepository<RegGoal> regGoalRepository
        {
            get
            {
                if (this._regGoalRepository == null)
                    this._regGoalRepository = new GenericRepository<RegGoal>(_context);
                return _regGoalRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for status repository.
        /// </summary>
        public GenericRepository<Status> StatusRepository
        {
            get
            {
                if (this._statusRepository == null)
                    this._statusRepository = new GenericRepository<Status>(_context);
                return _statusRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for timeSlot repository.
        /// </summary>
        public GenericRepository<TimeSlot> TimeSlotRepository
        {
            get
            {
                if (this._timeSlotRepository == null)
                    this._timeSlotRepository = new GenericRepository<TimeSlot>(_context);
                return _timeSlotRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for imageDetail repository.
        /// </summary>
        public GenericRepository<ImageDetail> ImageDetailRepository
        {
            get
            {
                if (this._imageDetailRepository == null)
                    this._imageDetailRepository = new GenericRepository<ImageDetail>(_context);
                return _imageDetailRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for adMedia repository.
        /// </summary>
        public GenericRepository<AdMedia> AdMediaRepository
        {
            get
            {
                if (this._adMediaRepository == null)
                    this._adMediaRepository = new GenericRepository<AdMedia>(_context);
                return _adMediaRepository;
            }
        }




        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}