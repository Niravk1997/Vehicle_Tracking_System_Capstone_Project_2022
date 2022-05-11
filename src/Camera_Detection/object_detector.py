
import cv2 as cv # OpenCV computer vision library
import numpy as np # Scientific computing library 
import requests

Server_IP = "192.168.0.57:3443"
Server_Begin_URL = "http://"
Server_End_URL = "/VRU_User_POST/Update_Camera/"

User_ID = 0

#  classes = ['person','bicycle','car','motorcycle','airplane' ,'bus','train','truck','boat' ,'traffic light','fire hydrant',
#    'stop sign','parking meter','bench','bird','cat','dog','horse','sheep','cow','elephant','bear','zebra','giraffe' ,
#    'backpack','umbrella','handbag' ,'tie','suitcase','frisbee' ,'skis','snowboard','sports ball' ,'kite',
#    'baseball bat','baseball glove','skateboard','surfboard','tennis rack','bottle','wine glass','cup','fork','knife',
#    'spoon','bowl','banana','apple' ,'sandwich','orange','broccoli','carrot','hot dog','pizza' ,'donut' ,'cake',
#    'chair' ,'couch' ,'potted plant','bed','dining table','toilet','tv','laptop','mouse','remote','keyboard',
#    'cell phone','microwave','oven','toaster','sink','refrigerator','book','clock','vase','scissors' ,'teddy bear',
#    'hair drier','toothbrush']

# Just use a subset of the classes
classes = ["unknown", "person", "unknown", "car", "motorcycle",
  "airplane", "bus", "train", "truck", "unknown", "unknown", "unknown",
  "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown",
  "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown",
  "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown",
  "unknown", "unknown", "unknown", "unknown", "unknown", "unknown",
  "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown",
  "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown",
  "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown",
  "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown",
  "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown",
  "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown" ]

# Colors we will use for the object labels
colors = np.random.uniform(0, 255, size=(len(classes), 3))

# Open the webcam
cam = cv.VideoCapture(0)

pb  = 'frozen_inference_graph.pb'
pbt = 'ssd_inception_v2_coco_2017_11_17.pbtxt'

# Read the neural network
cvNet = cv.dnn.readNetFromTensorflow(pb,pbt)   

while True:
  # Read in the frame
  ret_val, img = cam.read()
  rows = img.shape[0]
  cols = img.shape[1]
  cvNet.setInput(cv.dnn.blobFromImage(img, size=(300, 300), swapRB=True, crop=False))

  # Run object detection
  cvOut = cvNet.forward()


  Camera_JSON_Data = {'isCamera_Detected': 'false', 'Camera_Certainty': '0'}
  try:
    x = requests.post((Server_Begin_URL + Server_IP + Server_End_URL + str(User_ID)), data = Camera_JSON_Data)
  except:
    print("An exception occurred")

  # Go through each object detected and label it
  for detection in cvOut[0,0,:,:]:
    score = float(detection[2])
    if score > 0.3:

      idx = int(detection[1])   # prediction class index. 

      if classes[idx] == 'person':
        Camera_JSON_Data = {'isCamera_Detected': 'true', 'Camera_Certainty': round(score * 100, 2)}
        try:
          x = requests.post((Server_Begin_URL + Server_IP + Server_End_URL + str(User_ID)), data = Camera_JSON_Data)
        except:
          print("An exception occurred")
        left = detection[3] * cols
        top = detection[4] * rows
        right = detection[5] * cols
        bottom = detection[6] * rows
        cv.rectangle(img, (int(left), int(top)), (int(right), int(bottom)), (23, 230, 210), thickness=2)
           
        # draw the prediction on the frame
        label = "{}: {:.2f}%".format(classes[idx],score * 100)
        y = top - 15 if top - 15 > 15 else top + 15
        cv.putText(img, label, (int(left), int(y)),cv.FONT_HERSHEY_SIMPLEX, 0.5, colors[idx], 2)


  # Display the frame
  cv.imshow('Object Detection', img)

  # Press ESC to quit
  if cv.waitKey(1) == 27: 
    break 

# Stop filming
cam.release()

# Close down OpenCV
cv.destroyAllWindows()