import { Search } from "./components/search/Search";
import { About } from "./components/about/About";
import { Box } from "../../complib/layouts";
import { useState } from "react";
import { useTheme } from "styled-components";

export const Home = () => {
  const theme = useTheme();
  const [selected, setSelected] = useState<string>("");

  return (
    <Box
      flex={1}
      display={"flex"}
      flexWrap={"wrap"}
      bgColor={theme.tyle.color.sys.surface.base}
      color={theme.tyle.color.sys.surface.on}
      overflow={"auto"}
    >
      <Search selected={selected} setSelected={setSelected} />
      <About selected={selected} />
    </Box>
  );
};
