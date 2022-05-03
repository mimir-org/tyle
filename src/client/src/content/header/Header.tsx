import { useTheme } from "styled-components";
import { Box } from "../../complib/layouts";
import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { Logo } from "./components/Logo";
import { LibraryIcon } from "../../assets/icons/modules";
import { User } from "./components/User";

export const Header = () => {
  const { data, isLoading } = useGetCurrentUser();
  const theme = useTheme();

  return (
    <Box
      as={"header"}
      display={"flex"}
      alignItems={"center"}
      justifyContent={"space-between"}
      py={theme.typeLibrary.spacing.small}
      px={theme.typeLibrary.spacing.large}
      height={"60px"}
      bgColor={theme.typeLibrary.color.primary.base}
      color={theme.typeLibrary.color.primary.on}
      boxShadow={theme.typeLibrary.shadow.small}
      zIndex={10}
    >
      <Logo name={"Type library"} icon={LibraryIcon} />
      {!isLoading && <User name={`${data?.firstName} ${data?.lastName}`} />}
    </Box>
  );
};
