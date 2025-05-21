import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { MainLayout } from './components/layouts';
import AppRoutes from './routes/AppRoutes';
import { useAppDispatch } from 'app/hooks';
import { useGetUserProfileQuery } from 'features/auth/authApi';
import { useEffect } from 'react';
import { setUser } from 'features/auth/authSlice';
import { getAccessToken } from 'services/auth.service';

function App() {
  const dispatch = useAppDispatch()
  const { data, error, isLoading } = useGetUserProfileQuery()

  useEffect(() => {
    const accessToken = getAccessToken()
    if (accessToken && data) {
      dispatch(setUser(data.user))
    }
  }, [dispatch, data])

  return (
    <Router>
      <Routes>
        <Route element={<MainLayout />}>
          <Route index element={<AppRoutes />} />
          <Route path="/*" element={<AppRoutes />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
