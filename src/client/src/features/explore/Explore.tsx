import { About } from "features/explore/about/About";
import { SelectedInfo } from "features/explore/common/selectedInfo";
import { ExploreContainer } from "features/explore/Explore.styled";
import { Search } from "features/explore/search/Search";
import { useState } from "react";

export const Explore = () => {
  const [selected, setSelected] = useState<SelectedInfo>({ id: undefined });

  return (
    <ExploreContainer>
      <Search selected={selected} setSelected={setSelected} />
      <About selected={selected} />
    </ExploreContainer>
  );
};
