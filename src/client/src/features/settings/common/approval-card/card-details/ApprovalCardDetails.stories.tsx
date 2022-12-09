import { ComponentStory } from "@storybook/react";
import { ApprovalCardDetails } from "features/settings/common/approval-card/card-details/ApprovalCardDetails";

export default {
  title: "Features/Settings/Common/ApprovalCardDetails",
  component: ApprovalCardDetails,
};

const Template: ComponentStory<typeof ApprovalCardDetails> = (args) => <ApprovalCardDetails {...args} />;

export const Default = Template.bind({});
Default.args = {
  descriptors: {
    "E-mail": "jane.smith@organization.com",
    Organization: "Mimirorg Company",
    Purpose: "I want to create entities on behalf of my employer",
  },
};
