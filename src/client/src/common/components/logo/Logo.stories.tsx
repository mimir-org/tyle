import { ComponentStory } from "@storybook/react";
import { Logo } from "common/components/logo/Logo";

export default {
  title: "Common/Logo",
  component: Logo,
};

const Template: ComponentStory<typeof Logo> = (args) => <Logo {...args}></Logo>;

export const Default = Template.bind({});
Default.args = {
  width: "100px",
  height: "50px",
};
