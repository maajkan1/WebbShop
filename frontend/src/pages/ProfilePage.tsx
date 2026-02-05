import { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import { getUser, updateProfile } from '../api';
import { useAuth } from '../hooks/useAuth';
import type { UserProfileDto } from '../types';

function ProfilePage() {
  const { token, userId } = useAuth();
  const [profile, setProfile] = useState<UserProfileDto | null>(null);
  const [email, setEmail] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [status, setStatus] = useState('');
  const [error, setError] = useState('');

  useEffect(() => {
    if (!userId) return;

    const loadProfile = async () => {
      try {
        const data = await getUser(userId);
        setProfile(data);
        setEmail(data.email ?? '');
        setFirstName(data.firstName ?? '');
        setLastName(data.lastName ?? '');
      } catch (err: unknown) {
        setError(err instanceof Error ? err.message : 'Kunde inte hämta profil');
      }
    };

    loadProfile();
  }, [userId]);

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  const handleSubmit = async () => {
    setError('');
    setStatus('Saving...');

    if (!userId || !profile) {
      setStatus('');
      setError('Saknar användarprofil');
      return;
    }

    const payload: UserProfileDto = {
      ...profile,
      email,
      firstName,
      lastName,
    };

    try {
      await updateProfile(token, userId, payload);
      setStatus('Profilen uppdaterad');
    } catch (err: unknown) {
      setStatus('');
      setError(err instanceof Error ? err.message : 'Kunde inte uppdatera profil');
    }
  };

  return (
    <section className="profile">
      <div className="profile-card">
        <h2>Din profil</h2>
        <div className="profile-field">
          <label>E-post</label>
          <input value={email} onChange={e => setEmail(e.target.value)} />
        </div>
        <div className="profile-field">
          <label>Förnamn</label>
          <input value={firstName} onChange={e => setFirstName(e.target.value)} />
        </div>
        <div className="profile-field">
          <label>Efternamn</label>
          <input value={lastName} onChange={e => setLastName(e.target.value)} />
        </div>
        <button className="button primary" onClick={handleSubmit}>
          Spara ändringar
        </button>
        {status && <p className="status-text">{status}</p>}
        {error && <p className="error-text">{error}</p>}
      </div>
    </section>
  );
}

export default ProfilePage;
