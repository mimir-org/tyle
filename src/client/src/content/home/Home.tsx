import { useState } from "react";
import { useTheme } from "styled-components";
import { Box } from "../../complib/layouts";
import { About } from "./components/about/About";
import { Search } from "./components/search/Search";

export const Home = () => {
  const theme = useTheme();
  const [selected, setSelected] = useState<string>("");

  return (
    <Box
      flex={1}
      display={"flex"}
      flexWrap={"wrap"}
      gap={theme.tyle.spacing.multiple(24)}
      px={theme.tyle.spacing.multiple(12)}
      pt={theme.tyle.spacing.multiple(6)}
      pb={theme.tyle.spacing.xl}
      bgColor={theme.tyle.color.sys.background.base}
      color={theme.tyle.color.sys.background.on}
      overflow={"auto"}
    >
      <Search selected={selected} setSelected={setSelected} />
      <About selected={selected} />
    </Box>
  );
};
