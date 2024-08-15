using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    class Program
    {
        static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        class Reward
        {
            
        }

        static Reward GetRewardById(int id)
        {
            _lock.EnterReadLock();

            _lock.ExitReadLock();
            return null;
        }

        static Reward AddReward(int id)
        {
            _lock.EnterWriteLock();

            _lock.ExitWriteLock();
            return null;
        }

        static void Main(string[] args)
        {

        }
    }
}