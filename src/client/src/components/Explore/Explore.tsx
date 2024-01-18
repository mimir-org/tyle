import About from "components/About";
import Search from "components/Search";
import { useState } from "react";
import { SelectedInfo } from "types/selectedInfo";
import { ExploreContainer } from "./Explore.styled";

const Explore = () => {
  const [selected, setSelected] = useState<SelectedInfo>({ id: undefined });

  return (
    <ExploreContainer>
      <Search selected={selected} setSelected={setSelected} />
      <About selected={selected} />
    </ExploreContainer>
  );
};

export default Explore;
