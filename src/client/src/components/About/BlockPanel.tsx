import Box, { MotionBox } from "components/Box";
import { useTheme } from "styled-components";
import { BlockView } from "../../types/blocks/blockView";
import Heading from "../Heading";
import StateBadge from "../StateBadge";
import Divider from "../Divider";
import PanelPropertiesContainer from "./PanelPropertiesContainer";
import PanelSection from "./PanelSection";
import Text from "../Text";
import { Aspect } from "../../types/common/aspect";
import InfoItemButton from "../InfoItemButton";
import {
  mapAttributeViewsToInfoItems,
  mapTerminalTypeReferenceViewToBlockTerminalItems,
  mapRdlClassifiersToInfoItems,
  mapRdlPurposeToInfoItem,
  sortInfoItems,
  isNullUndefinedOrWhitespace,
} from "../../helpers/mappers.helpers";
import { getOptionsFromEnum } from "../../utils";
import { State } from "../../types/common/state";
import TerminalTable from "./TerminalTable";

interface BlockPanelProps {
  blockData: BlockView;
}
const BlockPanel = ({ blockData }: BlockPanelProps) => {
  const theme = useTheme();
  const states = getOptionsFromEnum(State);
  const tokens = [blockData.version, states[blockData.state].label];
  const attributesMapped = sortInfoItems(mapAttributeViewsToInfoItems(blockData.attributes.map((x) => x.attribute)));
  const classifiersMapped = mapRdlClassifiersToInfoItems(blockData.classifiers);
  const terminalsMapped = mapTerminalTypeReferenceViewToBlockTerminalItems(blockData.terminals);
  const purposeMapped = mapRdlPurposeToInfoItem(blockData.purpose);

  return (
    <MotionBox
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.tyle.spacing.xxxl}
      maxHeight={"100%"}
      overflow={"hidden"}
      {...theme.tyle.animation.fade}
    >
      <Box display={"grid"} rowGap={theme.tyle.spacing.xxl}>
        <Box display={"grid"}>
          <Box gridColumn={"1"}>
            <Heading as={"h2"}>{blockData.name}</Heading>
          </Box>
          <Box display={"flex"} gridColumn={"2"} justifyContent={"right"} alignItems={"center"}>
            {tokens && tokens.map((token, i) => <StateBadge key={i + token} state={token} />)}
          </Box>
        </Box>
        <Divider />
        <PanelPropertiesContainer>
          {!isNullUndefinedOrWhitespace(blockData.notation) && (
            <PanelSection title={"Notation"}>
              <Text>{blockData.notation}</Text>
            </PanelSection>
          )}
          {blockData.aspect !== null && (
            <PanelSection title={"Aspect"}>
              <Text>{Aspect[blockData.aspect]}</Text>
            </PanelSection>
          )}
          {!isNullUndefinedOrWhitespace(purposeMapped.id) && (
            <PanelSection title={"Purpose"}>
              <InfoItemButton key={blockData.purpose?.id} {...purposeMapped} />
            </PanelSection>
          )}
          {!isNullUndefinedOrWhitespace(blockData.description) && (
            <PanelSection title={"Description"}>
              <Text>{blockData.description}</Text>
            </PanelSection>
          )}
          {classifiersMapped.length > 0 && (
            <PanelSection title="Classifiers">
              {classifiersMapped.map((a, i) => (
                <InfoItemButton key={i} {...a} />
              ))}
            </PanelSection>
          )}
          {attributesMapped.length > 0 && (
            <PanelSection title="Attributes">
              {attributesMapped.map((a, i) => (
                <InfoItemButton key={i} {...a} />
              ))}
            </PanelSection>
          )}
          {terminalsMapped.length > 0 && (
            <PanelSection title="Terminals">
              <TerminalTable terminals={terminalsMapped} />
            </PanelSection>
          )}
        </PanelPropertiesContainer>
      </Box>
    </MotionBox>
  );
};

export default BlockPanel;
