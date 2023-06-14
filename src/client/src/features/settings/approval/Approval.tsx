import { Box, Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { ApprovalPlaceholder } from "features/settings/approval/placeholder/ApprovalPlaceholder";
import { ApprovalCard } from "features/settings/common/approval-card/ApprovalCard";
import { SettingsSection } from "features/settings/common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useQueryClient } from "@tanstack/react-query";
import { approvalKeys, useGetApprovals } from "external/sources/approval/approval.queries";
import { ApprovalDataCm, State } from "@mimirorg/typelibrary-types";
import { usePatchAspectObjectStateReject } from "external/sources/aspectobject/aspectObject.queries";
import { usePatchTerminalStateReject } from "external/sources/terminal/terminal.queries";
import { usePatchAttributeStateReject } from "../../../external/sources/attribute/attribute.queries";
import { usePatchUnitStateReject } from "../../../external/sources/unit/unit.queries";
import { usePatchRdsStateReject } from "../../../external/sources/rds/rds.queries";
import { usePatchQuantityDatumStateReject } from "../../../external/sources/datum/quantityDatum.queries";

export const Approval = () => {
  const queryClient = useQueryClient();
  const theme = useTheme();
  const { t } = useTranslation("settings");
  const approvals = useGetApprovals();
  const patchMutationRejectAspectObject = usePatchAspectObjectStateReject();
  const patchMutationRejectTerminal = usePatchTerminalStateReject();
  const patchMutationRejectAttribute = usePatchAttributeStateReject();
  const patchMutationRejectUnit = usePatchUnitStateReject();
  const patchMutationRejectQuantityDatum = usePatchQuantityDatumStateReject();
  const patchMutationRejectRds = usePatchRdsStateReject();
  const showPlaceholder = approvals?.data && approvals.data.length === 0;

  const onSubmit = () => {
    setTimeout(() => {
      queryClient.invalidateQueries(approvalKeys.lists());
    }, 500);
  };

  /*
   * Rejects an approval request
   * @param id the id of the approval request
   * @param state the state to set the approval request to
   * @param objectType the type of object the approval request is for
   * @see State
   */
  const onReject = (id: string, state: State, objectType: string) => {
    const data: ApprovalDataCm = { id: id, state: state };

    switch (objectType) {
      case "AspectObject":
        patchMutationRejectAspectObject.mutateAsync(data);
        break;
      case "Terminal":
        patchMutationRejectTerminal.mutateAsync(data);
        break;
      case "Attribute":
        patchMutationRejectAttribute.mutateAsync(data);
        break;
      case "Unit":
        patchMutationRejectUnit.mutateAsync(data);
        break;
      case "Quantity datum":
        patchMutationRejectQuantityDatum.mutateAsync(data);
        break;
      case "Rds":
        patchMutationRejectRds.mutateAsync(data);
        break;
      default:
        break;
    }

    setTimeout(() => {
      queryClient.invalidateQueries(approvalKeys.lists());
    }, 500);
  };

  return (
    <SettingsSection title={t("approval.title")}>
      {/* Approval */}
      <Text variant={"title-medium"} mb={theme.tyle.spacing.l}>
        {t("approval.approval")}
      </Text>
      <Flexbox flexDirection={"row"} flexWrap={"wrap"} gap={theme.tyle.spacing.xxxl}>
        {showPlaceholder && <ApprovalPlaceholder text={t("approval.placeholders.emptyApproval")} />}
        {approvals.data
          ?.filter((x) => x.state === State.Approve)
          .map((approval) => (
            <ApprovalCard key={`${approval.id}`} item={approval} onSubmit={onSubmit} onReject={onReject} />
          ))}
      </Flexbox>
      {/* Deletion */}
      {approvals.data?.find((x) => x.state === State.Delete) && (
        <Box mt={theme.tyle.spacing.xxl} pt={"12px"}>
          <Text variant={"title-medium"} mb={theme.tyle.spacing.l}>
            {t("approval.deletion")}
          </Text>
          <Flexbox flexDirection={"row"} flexWrap={"wrap"} gap={theme.tyle.spacing.xxxl}>
            {showPlaceholder && <ApprovalPlaceholder text={t("approval.placeholders.emptyApproval")} />}
            {approvals.data
              ?.filter((x) => x.state === State.Delete)
              .map((approval) => (
                <ApprovalCard key={`${approval.id}`} item={approval} onSubmit={onSubmit} onReject={onReject} />
              ))}
          </Flexbox>
        </Box>
      )}
    </SettingsSection>
  );
};
