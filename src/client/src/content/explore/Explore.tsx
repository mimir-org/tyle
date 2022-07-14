import { useState } from "react";
import { useTheme } from "styled-components";
import { Box } from "../../complib/layouts";
import { About } from "./components/about/About";
import { Search } from "./components/search/Search";

export const Explore = () => {
  const theme = useTheme();
  const [selected, setSelected] = useState("");

  return (
    <Box
      flex={1}
      display={"flex"}
      flexWrap={"wrap"}
      gap={`min(${theme.tyle.spacing.multiple(27)}, 12vw)`}
      px={`min(${theme.tyle.spacing.multiple(12)}, 6vw)`}
      pt={theme.tyle.spacing.multiple(6)}
      pb={theme.tyle.spacing.xl}
      overflow={"auto"}
    >
      <Search selected={selected} setSelected={setSelected} />
      <About selected={selected} />
    </Box>
  );
};
