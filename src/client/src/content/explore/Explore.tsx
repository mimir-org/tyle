import { useState } from "react";
import { About } from "./components/about/About";
import { Search } from "./components/search/Search";
import { ExploreContainer } from "./Explore.styled";

export const Explore = () => {
  const [selected, setSelected] = useState("");

  return (
    <ExploreContainer>
      <Search selected={selected} setSelected={setSelected} />
      <About selected={selected} />
    </ExploreContainer>
  );
};
