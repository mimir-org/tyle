import { BlockTerminalItem } from "../../types/blockTerminalItem";
import { State } from "../../types/common/state";
import styled from "styled-components/macro";
import { Heading, Text } from "@mimirorg/component-library";
import StateBadge from "../StateBadge";
import PanelPropertiesContainer from "./PanelPropertiesContainer";
import PanelSection from "./PanelSection";
import InfoItemButton from "../InfoItemButton";
import { InfoItem } from "../../types/infoItem";
import TerminalTable from "./TerminalTable";

interface previewPanelProps {
  name: string;
  description: string;
  tokens?: string[];
  terminals?: BlockTerminalItem[];
  attributes?: InfoItem[];
  state: State | string;
  kind: string | null;
}

const PreviewPanel = ({ name, description, tokens, terminals, attributes, state, kind }: previewPanelProps) => {
  const showAttributes = kind === "TerminalItem" || "BlockItem" && attributes && attributes.length > 0;
  const showTerminals = kind === "BlockItem" && terminals && terminals.length > 0;

  return (
    <GridContainer>
      <FlexContainer>
        <Heading as={"h2"}>{name}</Heading>
        {kind === "AttributeItem" ? (
          <StateBadge state={state} />
        ) : (
          tokens && tokens.map((token, i) => <StateBadge key={i + token} state={token} />)
        )}
      </FlexContainer>
      <Text useEllipsis ellipsisMaxLines={8}>
        {description}
      </Text>
      <PanelPropertiesContainer>
        {showAttributes && (
          <PanelSection title="Attributes">{attributes?.map((a, i) => <InfoItemButton key={i} {...a} />)}</PanelSection>
        )}
        {showTerminals && (
          <PanelSection title="Terminals">
            <TerminalTable terminals={terminals} />
          </PanelSection>
        )}
      </PanelPropertiesContainer>
    </GridContainer>
  );
};

export default PreviewPanel;

const GridContainer = styled.div`
  display: grid;
`;

const FlexContainer = styled.div`
  display: flex;
  flex-direction: row;
`;
