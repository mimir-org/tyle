import { useGetCurrentUser } from "api/user.queries";
import Flexbox from "components/Flexbox";
import Text from "components/Text";
import config from "config";
import { useTheme } from "styled-components";
import DocumentationButton from "./Documentation";
import FeedbackButton from "./FeedbackButton";
import HeaderContainer from "./Header.styles";
import HeaderHomeLink from "./HeaderHomeLink";
import LogoutButton from "./LogoutButton";
import SettingsButton from "./SettingsButton";
import ThemeButton from "./ThemeButton";
import UserInfo from "./UserInfo";
import UserMenu from "./UserMenu";

const Header = () => {
  const theme = useTheme();
  const userQuery = useGetCurrentUser();

  const userInitials = `${userQuery.data?.firstName?.[0]}${userQuery.data?.lastName?.[0]}`;
  const userFullName = `${userQuery.data?.firstName} ${userQuery.data?.lastName}`;
  const userRoles = userQuery.data?.roles;

  return (
    <HeaderContainer>
      <HeaderHomeLink />
      {!userQuery.isLoading && (
        <UserMenu name={userInitials}>
          <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.base}>
            <UserInfo name={userFullName} roles={userRoles} />
            <ThemeButton />
            <DocumentationButton />
            <FeedbackButton />
            <SettingsButton />
            <LogoutButton />
            <Flexbox alignItems={"center"} justifyContent={"center"} gap={theme.tyle.spacing.base}>
              <Text style={{ color: "gray" }} variant={"body-small"}>
                {"Tyle version: " + config.TYLE_VERSION}
              </Text>
            </Flexbox>
          </Flexbox>
        </UserMenu>
      )}
    </HeaderContainer>
  );
};

export default Header;
