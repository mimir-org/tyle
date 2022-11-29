import { ComponentStory } from "@storybook/react";
import { Logo } from "features/common/logo/Logo";

export default {
  title: "Features/Common/Logo",
  component: Logo,
};

const Template: ComponentStory<typeof Logo> = (args) => <Logo {...args}></Logo>;

export const Default = Template.bind({});
Default.args = {
  width: "100px",
  height: "50px",
};
