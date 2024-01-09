import Box, { MotionBox } from "components/Box";
import { useTheme } from "styled-components";
import Heading from "../Heading";
import StateBadge from "../StateBadge";
import Divider from "../Divider";
import PanelPropertiesContainer from "./PanelPropertiesContainer";
import PanelSection from "./PanelSection";
import Text from "../Text";
import InfoItemButton from "../InfoItemButton";
import { TerminalView } from "../../types/terminals/terminalView";
import {
  mapAttributeViewsToInfoItems,
  mapRdlClassifiersToInfoItems,
  mapRdlMediumToInfoItem,
  mapRdlPurposeToInfoItem,
  sortInfoItems,
} from "../../helpers/mappers.helpers";
import { getOptionsFromEnum } from "../../utils";
import { State } from "../../types/common/state";
import { Aspect } from "../../types/common/aspect";
import { Direction } from "../../types/terminals/direction";

interface TerminalPanelProps {
  terminalData: TerminalView;
}

export const TerminalPanel = ({ terminalData }: TerminalPanelProps) => {
  const theme = useTheme();
  const states = getOptionsFromEnum(State);
  const tokens = [terminalData.version, states[terminalData.state].label];
  const attributesMapped = sortInfoItems(mapAttributeViewsToInfoItems(terminalData.attributes.map((x) => x.attribute)));
  const classifiersMapped = mapRdlClassifiersToInfoItems(terminalData.classifiers);
  const purposeMapped = mapRdlPurposeToInfoItem(terminalData.purpose);
  const mediumMapped = mapRdlMediumToInfoItem(terminalData.medium);

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
            <Heading as={"h2"}>{terminalData.name}</Heading>
          </Box>
          <Box display={"flex"} gridColumn={"2"} justifyContent={"right"} alignItems={"center"}>
            {tokens && tokens.map((token, i) => <StateBadge key={i + token} state={token} />)}
          </Box>
        </Box>
        <Divider />
        <PanelPropertiesContainer>
          <PanelSection title={"Notation"}>
            <Text>{terminalData.notation}</Text>
          </PanelSection>

          <PanelSection title={"Aspect"}>
            <Text>{terminalData.aspect !== null ? Aspect[terminalData.aspect] : ""}</Text>
          </PanelSection>

          <PanelSection title={"Purpose"}>
            <InfoItemButton key={terminalData.purpose?.id} {...purposeMapped} />
          </PanelSection>
          <PanelSection title={"Description"}>
            <Text>{terminalData.description}</Text>
          </PanelSection>
          <Box display={"grid"}>
            <Box gridColumn={"1"}>
              <PanelSection title={"Medium"}>
                <InfoItemButton key={terminalData.medium?.id} {...mediumMapped} />
              </PanelSection>
            </Box>
            <Box gridColumn={"2"}>
              <PanelSection title={"Qualifier"}>
                <Text>{Direction[terminalData.qualifier]}</Text>
              </PanelSection>
            </Box>
          </Box>
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
        </PanelPropertiesContainer>
      </Box>
    </MotionBox>
  );
};
