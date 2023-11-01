import { About } from "components/About/About";
import { SelectedInfo } from "common/types/selectedInfo";
import { ExploreContainer } from "components/Explore/Explore.styled";
import { Search } from "components/Search/Search";
import { useState } from "react";

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