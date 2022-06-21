import { useTheme } from "styled-components";
import { LibraryIcon } from "../../assets/icons/modules";
import { Box } from "../../complib/layouts";
import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { Logo } from "./components/Logo";
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
      py={theme.tyle.spacing.base}
      px={theme.tyle.spacing.xxxl}
      height={"56px"}
      bgColor={theme.tyle.color.sys.primary.base}
      color={theme.tyle.color.sys.primary.on}
      boxShadow={theme.tyle.shadow.small}
      zIndex={10}
    >
      <Logo name={":TYLE"} icon={LibraryIcon} />
      {!isLoading && <User name={`${data?.firstName} ${data?.lastName}`} />}
    </Box>
  );
};
