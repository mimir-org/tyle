import Button from "components/Button";
import Flexbox from "components/Flexbox";
import Text from "components/Text";
import Tooltip from "components/Tooltip";
import { useSubmissionToast } from "helpers/form.helpers";
import { useTheme } from "styled-components";
import { AttributeView } from "types/attributes/attributeView";
import { BlockView } from "types/blocks/blockView";
import { State } from "types/common/state";
import { TerminalView } from "types/terminals/terminalView";
import { usePatchStateMutation } from "./ApprovalCardForm.helpers";

export interface ApprovalCardFormProps {
  item: AttributeView | TerminalView | BlockView;
  itemType: "attribute" | "terminal" | "block";
  disabledButton: boolean;
}

const ApprovalCardForm = ({ item, itemType, disabledButton = true }: ApprovalCardFormProps) => {
  const theme = useTheme();

  const patchStateMutation = usePatchStateMutation(item, itemType);

  const toast = useSubmissionToast(itemType);

  return (
    <Flexbox flexFlow={"row"} justifyContent={"space-between"} style={{ marginTop: "8px" }}>
      <Text variant={"body-large"}>{`Requesting to be approved.`}</Text>
      <Flexbox justifyContent={"center"} alignItems={"center"} flexFlow="row" gap={theme.tyle.spacing.base}>
        <Button
          dangerousAction
          type={"button"}
          onClick={() => toast(patchStateMutation.mutateAsync({ state: State.Draft }))}
        >
          Reject
        </Button>
        {!disabledButton ? (
          <Button type={"button"} onClick={() => toast(patchStateMutation.mutateAsync({ state: State.Approved }))}>
            Approve
          </Button>
        ) : (
          <Tooltip
            content={<Text>This type cannot be approved because it references types that are not approved</Text>}
          >
            <Button disabled={true} type={"button"}>
              Approve
            </Button>
          </Tooltip>
        )}
      </Flexbox>
    </Flexbox>
  );
};

export default ApprovalCardForm;
