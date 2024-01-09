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
  mapBlockTerminalLibCmsToBlockTerminalItems,
  mapRdlClassifiersToInfoItems,
  mapRdlPurposeToInfoItem,
  sortInfoItems,
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
  const terminalsMapped = mapBlockTerminalLibCmsToBlockTerminalItems(blockData.terminals);
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
          <PanelSection title={"Notation"}>
            <Text>{blockData.notation}</Text>
          </PanelSection>
          <PanelSection title={"Aspect"}>
            <Text>{blockData.aspect !== null ? Aspect[blockData.aspect] : ""}</Text>
          </PanelSection>
          <PanelSection title={"Purpose"}>
            <InfoItemButton key={blockData.purpose?.id} {...purposeMapped} />
          </PanelSection>
          <PanelSection title={"Description"}>
            <Text>{blockData.description}</Text>
          </PanelSection>
          <PanelSection title="Classifiers">
            {classifiersMapped.map((a, i) => (
              <InfoItemButton key={i} {...a} />
            ))}
          </PanelSection>
          <PanelSection title="Attributes">
            {attributesMapped.map((a, i) => (
              <InfoItemButton key={i} {...a} />
            ))}
          </PanelSection>
          <PanelSection title="Terminals">
            <TerminalTable terminals={terminalsMapped} />
          </PanelSection>
        </PanelPropertiesContainer>
      </Box>
    </MotionBox>
  );
};

export default BlockPanel;
