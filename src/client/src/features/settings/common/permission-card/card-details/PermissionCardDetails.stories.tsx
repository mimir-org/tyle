import { ComponentStory } from "@storybook/react";
import { PermissionCardDetails } from "features/settings/common/permission-card/card-details/PermissionCardDetails";

export default {
  title: "Features/Settings/Common/PermissionCardDetails",
  component: PermissionCardDetails,
};

const Template: ComponentStory<typeof PermissionCardDetails> = (args) => <PermissionCardDetails {...args} />;

export const Default = Template.bind({});
Default.args = {
  descriptors: {
    "E-mail": "jane.smith@organization.com",
    Organization: "Mimirorg Company",
    Purpose: "I want to create entities on behalf of my employer",
  },
};
