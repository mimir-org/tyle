import { useGetAttributesByState } from "api/attribute.queries";
import { useGetBlocksByState } from "api/block.queries";
import { useGetTerminalsByState } from "api/terminal.queries";
import Flexbox from "components/Flexbox";
import SettingsSection from "components/SettingsSection";
import Text from "components/Text";
import { useTheme } from "styled-components";
import { State } from "types/common/state";
import ApprovalCard from "./ApprovalCard";
import ApprovalPlaceholder from "./ApprovalPlaceholder";

const Approval = () => {
  const theme = useTheme();
  const attributesInReview = useGetAttributesByState(State.Review);
  const terminalsInReview = useGetTerminalsByState(State.Review);
  const blocksInReview = useGetBlocksByState(State.Review);
  const showPlaceholder =
    attributesInReview?.data &&
    attributesInReview.data.length === 0 &&
    terminalsInReview?.data &&
    terminalsInReview.data.length === 0 &&
    blocksInReview?.data &&
    blocksInReview.data.length === 0;

  return (
    <SettingsSection title="Approval">
      <Text variant={"title-medium"} spacing={{ mb: theme.tyle.spacing.l }}>
        Types ready for approval
      </Text>
      <Flexbox flexDirection={"row"} flexWrap={"wrap"} gap={theme.tyle.spacing.xxxl}>
        {showPlaceholder && <ApprovalPlaceholder text="There is no types ready for approval" />}
        {attributesInReview.data?.map((x) => <ApprovalCard key={x.id} item={x} itemType={"attribute"} />)}
        {terminalsInReview.data?.map((x) => <ApprovalCard key={x.id} item={x} itemType={"terminal"} />)}
        {blocksInReview.data?.map((x) => <ApprovalCard key={x.id} item={x} itemType={"block"} />)}
      </Flexbox>
    </SettingsSection>
  );
};

export default Approval;
