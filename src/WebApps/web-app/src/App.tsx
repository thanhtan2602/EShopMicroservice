import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { MainLayout } from './components/layouts';
import AppRoutes from './routes/AppRoutes';
import { useAppDispatch } from 'app/hooks';
import { useGetCurrentUserQuery } from 'features/auth/authApi';
import { useEffect } from 'react';
import { setUser } from 'features/auth/authSlice';

function App() {
  const dispatch = useAppDispatch()
  const { data, error, isLoading } = useGetCurrentUserQuery()

  useEffect(() => {
    if (data) {
      dispatch(setUser(data))
    } else if (error) {
      dispatch(setUser(null))
    }
  }, [data, error])

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
