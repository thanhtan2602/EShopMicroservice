// /auth/google/callback.tsx
import { useEffect } from 'react';

const GoogleCallback = () => {
  useEffect(() => {
    const urlParams = new URLSearchParams(window.location.search);
    const accessToken = urlParams.get('accessToken');
    const refreshToken = urlParams.get('refreshToken');
    const user = JSON.parse(decodeURIComponent(urlParams.get('user') || '{}'));

    window.opener?.postMessage(
      { accessToken, refreshToken, user },
      'http://localhost:3001'
    );

    window.close();
  }, []);

  return <p>Đang xác thực...</p>;
};

export default GoogleCallback;
