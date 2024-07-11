import ProfileInfo from "./components/info";
import ProfilePageContainer from "./components/profilePageContainer";
import ProfileSidebar from "./components/sidebar/profileSidebar";
import { useProfile } from "../../hooks/profile/useProfile";
import { Navigate, Route, Routes } from "react-router-dom";
import MyTests from "./components/myTests.tsx";
import PassedTests from "./components/passedTests.tsx";

/**
 * Страницы профиля пользователя
 */
function ProfilePage() {
  const [profile, loading] = useProfile();
  if (profile == null && !loading) return <Navigate to="/login"></Navigate>;
  return (
    <>
      <ProfileSidebar></ProfileSidebar>
      <ProfilePageContainer>
        <Routes>
          <Route path="/" element={<ProfileInfo />} />
          <Route path="/myTests" element={<MyTests />} />
          <Route path="/passedTests" element={<PassedTests />} />
          <Route path="*" element={<Navigate to="/profile" />} />
        </Routes>
      </ProfilePageContainer>
    </>
  );
}

export default ProfilePage;
