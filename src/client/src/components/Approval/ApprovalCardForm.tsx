import { Button, Flexbox, Text, Tooltip } from "@mimirorg/component-library";
import { useSubmissionToast } from "helpers/form.helpers";
import { useTheme } from "styled-components";
import { AttributeView } from "types/attributes/attributeView";
import { BlockView } from "types/blocks/blockView";
import { State } from "types/common/state";
import { TerminalView } from "types/terminals/terminalView";
import { usePatchStateMutation } from "./ApprovalCardForm.helpers";
import React from "react";

export interface ApprovalCardFormProps {
  item: AttributeView | TerminalView | BlockView;
  itemType: "attribute" | "terminal" | "block";
  dissabledButton: boolean;
}

const ApprovalCardForm = ({ item, itemType, dissabledButton = true }: ApprovalCardFormProps) => {
  const theme = useTheme();

  const patchStateMutation = usePatchStateMutation(item, itemType);

  const toast = useSubmissionToast(itemType);

  return (
    <Flexbox flexFlow={"row"} justifyContent={"space-between"} style={{ marginTop: "8px" }}>
      <Text variant={"body-large"}>{`Requesting to be approved.`}</Text>
      <Flexbox justifyContent={"center"} alignItems={"center"} flexFlow="row" gap={theme.mimirorg.spacing.base}>
        <Button
          dangerousAction
          type={"button"}
          onClick={() => toast(patchStateMutation.mutateAsync({ state: State.Draft }))}
        >
          Reject
        </Button>
        {!dissabledButton ? (

          <Button  type={"button"} onClick={() => toast(patchStateMutation.mutateAsync({ state: State.Approved }))}>
            Approve
          </Button>
        ) : (
          <Tooltip content={<Text>Please review the other types before approvoing this</Text>}>
          <Button disabled={true} type={"button"} onClick={() => toast(patchStateMutation.mutateAsync({ state: State.Approved }))}>
            Approve
          </Button>
          </Tooltip>

        )}
      </Flexbox>
    </Flexbox>
  );
};

export default ApprovalCardForm;
