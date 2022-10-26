import { ComponentStory } from "@storybook/react";
import { Loader } from "./Loader";

export default {
  title: "Common/Loader",
  component: Loader,
};

const Template: ComponentStory<typeof Loader> = () => <Loader />;

export const Default = Template.bind({});
