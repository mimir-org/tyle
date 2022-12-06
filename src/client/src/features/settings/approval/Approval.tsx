import { Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { ApprovalPlaceholder } from "features/settings/approval/placeholder/ApprovalPlaceholder";
import { ApprovalCard } from "features/settings/common/approval-card/ApprovalCard";
import { SettingsSection } from "features/settings/common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { ApprovalCm } from "common/types/approvalCm";
import { State } from "@mimirorg/typelibrary-types";

export const Approval = () => {
  const theme = useTheme();
  const { t } = useTranslation("settings");
  // const pendingUsersQuery = useGetPendingUsers();

  // const users = pendingUsersQuery.data?.sort((a, b) => a.firstName.localeCompare(b.firstName)) ?? [];

  const approvals = [] as ApprovalCm[];
  const approval: ApprovalCm = {
    id: "fdrertfg",
    name: "Test approval",
    description: "Approval description",
    objectType: "Node",
    state: State.ApproveCompany,
    companyId: 1,
    companyName: "Mimirorg Company",
    userId: "",
    userName: ""
  };

  approvals.push(approval);

  const showPlaceholder = approvals && approvals.length === 0;

  return (
    <SettingsSection title={t("approval.title")}>
      <Text variant={"title-medium"} mb={theme.tyle.spacing.l}>
        {t("approval.subtitle")}
      </Text>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xxxl}>
        {showPlaceholder && <ApprovalPlaceholder text={t("approval.placeholders.emptyApproval")} />}
        {approvals.map((approval) => (
          <ApprovalCard key={approval.id} item={approval} />      
        ))}
      </Flexbox>
    </SettingsSection>
  );
};
