import { useTheme } from "styled-components";
import { BlockTerminalItem } from "../../types/blockTerminalItem";
import { State } from "../../types/common/state";
import { InfoItem } from "../../types/infoItem";
import Box from "../Box";
import Divider from "../Divider";
import Heading from "../Heading";
import InfoItemButton from "../InfoItemButton";
import StateBadge from "../StateBadge";
import Text from "../Text";
import PanelPropertiesContainer from "./PanelPropertiesContainer";
import PanelSection from "./PanelSection";
import TerminalTable from "./TerminalTable";

interface PreviewPanelProps {
  name: string;
  description: string;
  tokens?: string[];
  terminals?: BlockTerminalItem[];
  attributes?: InfoItem[];
  state: State | string;
  kind: string | null;
}

const PreviewPanel = ({ name, description, tokens, terminals, attributes, state, kind }: PreviewPanelProps) => {
  const theme = useTheme();
  const showAttributes = kind === "TerminalItem" || ("BlockItem" && attributes && attributes.length > 0);
  const showTerminals = kind === "BlockItem" && terminals && terminals.length > 0;

  return (
    <Box display={"grid"} rowGap={theme.tyle.spacing.xxl}>
      <Box display={"grid"}>
        <Box gridColumn={"1"}>
          <Heading as={"h2"}>{name}</Heading>
        </Box>
        <Box display={"flex"} gridColumn={"2"} justifyContent={"right"} alignItems={"center"}>
          {kind === "AttributeItem" ? (
            <StateBadge state={state} />
          ) : (
            tokens && tokens.map((token, i) => <StateBadge key={i + token} state={token} />)
          )}
        </Box>
      </Box>
      <Divider />
      <PanelPropertiesContainer>
        <PanelSection title={"Description"}>
          <Text>{description}</Text>
        </PanelSection>
      </PanelPropertiesContainer>
      {showAttributes && (
        <PanelPropertiesContainer>
          <PanelSection title="Attributes">{attributes?.map((a, i) => <InfoItemButton key={i} {...a} />)}</PanelSection>
        </PanelPropertiesContainer>
      )}
      {showTerminals && (
        <PanelPropertiesContainer>
          <PanelSection title="Terminals">
            <TerminalTable terminals={terminals} />
          </PanelSection>
        </PanelPropertiesContainer>
      )}
    </Box>
  );
};

export default PreviewPanel;
