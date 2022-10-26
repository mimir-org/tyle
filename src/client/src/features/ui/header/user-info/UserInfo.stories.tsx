import { ComponentStory } from "@storybook/react";
import { UserInfo } from "./UserInfo";

export default {
  title: "UI/Header/UserInfo",
  component: UserInfo,
};

const Template: ComponentStory<typeof UserInfo> = (args) => <UserInfo {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Threepwood",
  roles: ["Global administrator"],
  permissions: ["Company A: Manage", "Company B: Manage"],
};
