import { useState } from "react";
import { About } from "./about/About";
import { Search } from "./search/Search";
import { ExploreContainer } from "./Explore.styled";
import { SelectedInfo } from "./common/selectedInfo";

export const Explore = () => {
  const [selected, setSelected] = useState<SelectedInfo>({ id: "" });

  return (
    <ExploreContainer>
      <Search selected={selected} setSelected={setSelected} />
      <About selected={selected} />
    </ExploreContainer>
  );
};
